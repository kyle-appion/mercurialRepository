using System;
using Security;
using Foundation;

namespace ION.IOS.App {
 
	public class KeychainAccess
	{
	    public static string ValueForKey(string key)
	    {
	        var record = ExistingRecordForKey (key);
	        SecStatusCode resultCode;
	        var match = SecKeyChain.QueryAsRecord(record, out resultCode);
	
	        if (resultCode == SecStatusCode.Success)
	            return NSString.FromData (match.ValueData, NSStringEncoding.UTF8);
	        else
	            return String.Empty;
	    }
	
	    public static void SetValueForKey(string value, string key) 
	    {
	        var record = ExistingRecordForKey (key);            
	        if (string.IsNullOrEmpty(value))
	        {
	            if (!string.IsNullOrEmpty(ValueForKey(key)))
	                RemoveRecord(record);
	
	            return;
	        }
	
	        // if the key already exists, remove it
	        if (!string.IsNullOrEmpty(ValueForKey(key)))
	            RemoveRecord(record);
	
	        var result = SecKeyChain.Add(CreateRecordForNewKeyValue(key, value));
	        if (result != SecStatusCode.Success)
	        {
	            Console.WriteLine("Couldn't add record to keychain");
	        }
	    }	 
	
	    private static SecRecord CreateRecordForNewKeyValue(string key, string value)
	    {
	        return new SecRecord(SecKind.GenericPassword)
	        {
	            Account = key,
	            Label = key,
	            ValueData = NSData.FromString(value, NSStringEncoding.UTF8),
	        };
	    }
	
	    private static SecRecord ExistingRecordForKey(string key)
	    {
	        return new SecRecord(SecKind.GenericPassword)
	        {
	            Account = key,
	            Label = key,
	        };
	    }
	
	    public static bool RemoveRecord(SecRecord record)
	    {
	        var result = SecKeyChain.Remove(record);
	        if (result != SecStatusCode.Success)
	        {
	            Console.WriteLine("Couldn't Remove record from keychain");
	        }
	
	        return true;
	    }
	}
}

