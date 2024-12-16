<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/GoogleSearch.ascx.cs" Inherits="Web.FrontEnd.Modules.GoogleSearch" %>

<style type="text/css">
.form-inline td{padding:0px !important; border:0 !important}
.form-inline input{height:24px !important}
.gsc-input-box{border:0 !important}</style>
<div class="form-inline">
	<div class="gcse-searchbox-only" data-queryparametername="sq" data-resultsurl="<%=ResultsUrl %>" enableautocomplete="true">
		&nbsp;</div>
	<script async src="https://cse.google.com/cse.js?cx=<%= Key %>"></script>
</div>