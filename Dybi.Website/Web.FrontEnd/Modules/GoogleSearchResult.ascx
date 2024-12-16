<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoogleSearchResult.ascx.cs" Inherits="Web.FrontEnd.Modules.GoogleSearchResult" %>

<script async src="https://cse.google.com/cse.js?cx=<%= Key %>"></script>
<div class="gcse-searchresults-only" data-queryParameterName="sq"></div>