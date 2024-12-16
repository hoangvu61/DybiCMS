<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/GoogleSearch.ascx.cs" Inherits="Web.FrontEnd.Modules.GoogleSearch" %>

<style type="text/css">
.form-inline td{padding:0px !important; border:0 !important}
.form-inline input{height:24px !important}
.gsc-input-box{background: #fff url(https://static.tumblr.com/ftv85bp/MIXmud4tx/search-icon.png) no-repeat 9px center;border: solid 1px #ccc;padding: 9px 10px 9px 32px;-webkit-border-radius: 10em;-moz-border-radius: 10em;border-radius: 10em;-webkit-transition: all .5s;-moz-transition: all .5s;transition: all .5s;-webkit-box-sizing: content-box; margin-bottom:20px}
.gsc-search-button{display:none}
</style>
<div class="search">
    <div class="form-inline">
	    <div class="gcse-searchbox-only" data-queryparametername="sq" data-resultsurl="<%=ResultsUrl %>" enableautocomplete="true">
		    &nbsp;</div>
	    <script async src="https://cse.google.com/cse.js?cx=<%= Key %>"></script>
    </div>
</div>