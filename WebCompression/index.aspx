<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebCompression.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta name="description" content="">
<meta name="author" content="">
<title>The Compression Project</title>
<link rel="icon" href="images/favicon.ico">
<!-- Bootstrap Core CSS -->
<link href="stylesheets/css/bootstrap.min.css" rel="stylesheet">
<!-- Custom CSS -->
<link href="stylesheets/css/theme.css" rel="stylesheet">
<!-- Custom Fonts -->
<link href="stylesheets/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
<link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,700,400italic,700italic" rel="stylesheet" type="text/css">
<link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">
<!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
<!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body id="page-top" data-spy="scroll" data-target=".navbar-fixed-top">
<!-- Navigation -->
<nav class="navbar navbar-custom navbar-fixed-top" role="navigation">
<div class="container">
	<div class="navbar-header">
		<button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-main-collapse">
		<i class="fa fa-bars"></i>
		</button>
		<a class="navbar-brand page-scroll" href="index.html">
		The Compression Project </a>
	</div>
	<!-- Collect the nav links, forms, and other content for toggling -->
	<div class="collapse navbar-collapse navbar-right navbar-main-collapse">
		<ul class="nav navbar-nav">
			<li>
			<a href="index.aspx">Home</a>
			</li>
			<li>
			<a href="compress.aspx">Compress</a>
			</li>
			<li>
			<a href="extract.aspx">Extract</a>
			</li>
		</ul>
	</div>
	<!-- /.navbar-collapse -->
</div>
<!-- /.container -->
</nav>
<!-- Intro Header -->
<header class="intro">
<div class="intro-body">
	<div class="container">
		<div class="row">
			<div class="col-md-8 col-md-offset-2">
				<h1 class="brand-heading">The Compression Project</h1>
				<p class="intro-text">
					Compression using Artificial Neural Networks
				</p>
				<a href="#about" class="btn btn-circle page-scroll">
				<i class="fa fa-angle-double-down animated"></i>
				</a>
			</div>
		</div>
	</div>
</div>
</header>
<!-- About Section -->
<section id="about">
<div class="container content-section text-center">
	<div class="row">
		<h2>About</h2>
		<div class="col-lg-8 col-lg-offset-2">
			<p>
				The Compression Project is a website that allows you to test out compression of files using artificial neural networks. You can upload images, text files and download a compressed version. You will then be able to extract the original data by uploading the compressed version. All you need to do is simply log in or register.
			</p>
			<p>
				<a href="#" class="btnghost"><i class="fa fa-user"></i> Log in</a>
			</p>
		</div>
	</div>
</div>
</section>
<!-- Footer -->
<footer>
<div class="container text-center">
	<p class="credits">
		Copyright &copy; The Compression Project 2017<br/>
		"Aries" Template by WowThemes.net
	</p>
</div>
</footer>
<!-- jQuery -->
<script src="scripts/js/jquery.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="scripts/js/bootstrap.min.js"></script>
<!-- Plugin JavaScript -->
<script src="scripts/js/jquery.easing.min.js"></script>
<!-- Custom Theme JavaScript -->
<script src="scripts/js/theme.js"></script>
</body>
</html>
