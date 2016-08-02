INSERT INTO 'DeviceRow' (id, serialNumber, name, lastConnected, connectionAddress, protocol)
  WHERE EXISTS (SELECT id, serialNumber, name, lastConnected, connectionAddress, protocol FROM 'Device');

DROP TABLE IF EXISTS 'Device';