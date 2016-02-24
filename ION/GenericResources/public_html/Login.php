<!DOCTYPE html>
<html>
<head>
<title>Do you even TXSTATE?</title>
<meta http-equiv="content-type" content="text/html; charset=utf-8" >
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<style type="text/css">
.loginHeader {
	background-color: #F3B972;
	text-align: center;
	width: 100%;
	height: 150px;
}
.homeButtons{
	width: 50%;
	margin-left: 25%;
}
.shome{
	align-content: center;
	margin-left: 45%;
}
.ghome{
	align-content: center;
margin-left: 45%;
}
.ahome{
	align-content: center;
margin-left: 45%;
}
.bhome{
	align-content: center;
margin-left: 45%;
}
.yhome{
	align-content: center;
margin-left: 45%;
}

</style>
</head>

<script>
	var stateObj = { foo: "Login.php" };
	history.replaceState(stateObj, "Login Page", "Login.php");
</script>

<div class="loginHeader">
</div>

<h1 style="margin-left: 40%;">Hello You Awesome Person!</h1>

<body>
<div class="homeButtons">
	<div class="shome" id="shome">
		<input type="button" value="Sang's Home" onclick="document.location.href='sangM/start.html'">
	</div>
	<div class="ghome" id="ghome">
		<input type="button" value="Gloria's Home" onclick="document.location.href='gloriaJ/start.html'">
	</div>
	<div class="ahome" id="ahome">
		<input type="button" value="Alex's Home" onclick="document.location.href='alexG/start.html'">
	</div>
	<div class="bhome" id="bhome">
		<input type="button" value="Bonnie's Home" onclick="document.location.href='bonnieW/start.html'">
	</div>
	<div class="yhome" id="yhome">
		<input type="button" value="Yinebeb's Home" onclick="document.location.href='yineZ/start.html'">
	</div>
	<div style="height: 50px;"></div>
	<div class="yhome">
		<input type="button" value="Go Back" onclick="document.location.href='home.html'">
	</div>
</div>

</body>
</html>

<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);
 ?>
