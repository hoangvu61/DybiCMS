<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/GoogleSearch.ascx.cs" Inherits="Web.FrontEnd.Modules.GoogleSearch" %>

<style type="text/css">
.form-inline td{padding:0px !important; border:0 !important}
.form-inline input{height:24px !important}
#___gcse_0{width:100%; margin-top:10px}
</style>


<!-- The Modal -->
<div class="modal" id="searchModal">
  <div class="modal-dialog modal-lg">
    <div class="modal-content"> 

      <!-- Modal Header -->
      <div class="modal-header">
          <h4 class="modal-title">
              <%=Title %>
          </h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

      <!-- Modal body -->
      <div class="modal-body">
          <div class="form-inline googlesearch">
	        <div class="gcse-searchbox-only" data-queryparametername="sq" data-resultsurl="<%=ResultsUrl %>" enableautocomplete="true">
		        &nbsp;</div>
	        <script async src="https://cse.google.com/cse.js?cx=<%= Key %>"></script>
        </div>
      </div>
    </div>
  </div>
</div>