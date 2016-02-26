<?php
		session_start();
		$hostname="localhost";
		$username="ktjohnson";
		$password="Workhard247!!";
		$dbname="TeamProjects";
		$usertable="Users";
		$yourfield = "UID";
		$connection = mysql_connect($hostname, $username, $password);

		mysql_select_db($dbname, $connection);

		$lname = $_GET['uname'];
		$lpass = $_GET['pword'];

		$query = "SELECT UID FROM $usertable WHERE u_name = '$lname' AND pass_w = '$lpass'";

		$result = mysql_query($query);
	 	$num_rows = mysql_num_rows($result);

		if($num_rows != 1){
			header('Location: http://www.buildtechhere.com');
		}

		if($row = mysql_fetch_assoc($result)){
			$_SESSION["userID"] = $row["UID"];
			if($row["UID"] == 1){
				$_SESSION["profile"] = "Kyle";
			} elseif ($row["UID"] == 2) {
				$_SESSION["profile"] = "Sang";
			} elseif ($row["UID"] == 3) {
				$_SESSION["profile"] = "Alex";
			} elseif ($row["UID"] == 4) {
				$_SESSION["profile"] = "Bonnie";
			} elseif ($row["UID"] == 5) {
				$_SESSION["profile"] = "Jared";
			} elseif ($row["UID"] == 6) {
				$_SESSION["profile"] = "Yinebeb";
			} elseif ($row["UID"] == 7) {
				$_SESSION["profile"] = "Gloria";
			}
		}
?>

<!DOCTYPE html>
<html>
<head>
<title>You've Made It!</title>
<meta http-equiv="content-type" content="text/html; charset=utf-8" >
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<script src='http://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js'></script>
<style type="text/css">
.loginHeader {
	background-color: #06457B;
	text-align: center;
	width: 100%;
	height: 10em;
	border-radius: 3px;
}
.homeButtons{
	width: 50%;
	margin-left: 25%;
}
.groupOption{
	width: 8em;
  height: 2em;
  background-color: #5797B8;
  float: right;
  text-align: center;
  line-height: 2em;
  cursor: pointer;
  border-radius: 5px;
}
.groupStyle{
	display: inline-block;
	vertical-align: middle;
	line-height: normal;
}
.logoutOption{
	width: 8em;
  height: 2em;
  background-color: #5797B8;
  float: right;
  text-align: center;
  line-height: 2em;
  cursor: pointer;
  border-radius: 5px;
}
.logoutStyle{
	display: inline-block;
	vertical-align: middle;
	line-height: normal;
}
.optionTable{
	float: right;
}
</style>

</head>

<script>
	var stateObj = { foo: "Login.php" };
	history.replaceState(stateObj, "Login Page", "Login.php");
</script>

<div class="loginHeader">
	<table class="optionTable">
		<tr>
			<td>
				<div class="groupOption"><span class="groupStyle">Group Area</span></div>
			</td>
		</tr>
		<tr>
			<td>
				<div class="logoutOption"><span class"logoutStyle">Log Out</span></div>
			</td>
		</tr>
	</table>
	</br>
	<h1 style="text-align: center;">Hello You Awesome Person!</h1>
</div>
<body>

</body>
</html>
<script>
  function logOut(){
    window.location = "http://www.buildtechhere.com/"
  }
</script>
<script>
  $(document).ready(function(){
    $('.groupOption').mousedown(function(){
      $('.groupOption').css('background-color', "white");
    });

    $('.groupOption').click(function(){
      window.location.href = "DropZone/Welcome.php";
    });

    $('.logoutOption').mousedown(function(){
      $('.logoutOption').css('background-color', "white");
    });

    $('.logoutOption').click(function(){
      window.location = "http://www.buildtechhere.com/"
    });

    $(document).on('mouseup', function(){
      $('.groupOption').css('background-color', "#5797B8");
      $('.logoutOption').css('background-color', "#5797B8");
    });
  });
</script>
