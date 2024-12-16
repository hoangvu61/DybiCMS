<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
	<link rel="stylesheet" href="/Templates/T12/css/jquery.fancybox.css">

    <style>
        .row {
          display: flex;
          flex-wrap: wrap;
          margin:60px 0px !important
        }

        /* Create four equal columns that sits next to each other */
        .column {
          flex: 25%;
          max-width: 25%;
        }

        .thumb{padding: 0 4px;}
        .column img {
          margin-top: 8px;
          vertical-align: middle;
        }

        /* Responsive layout - makes a two column-layout instead of four columns */
        @media (max-width: 800px) {
          .column {
            flex: 33%;
            max-width: 50%;
          }
        }

        /* Responsive layout - makes the two columns stack on top of each other instead of next to each other */
        @media (max-width: 600px) {
          .column {
            flex: 50%;
            max-width: 100%;
          }
        }
    </style>
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuAlbum").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <section class="section-all" style="padding:100px 0px">
        <div class="container">
            <VIT:Position runat="server" ID="psContent"></VIT:Position>
        </div>
    </section>

</asp:Content>