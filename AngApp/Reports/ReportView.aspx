﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="AngApp.Reports.ReportView" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>        
       <CR:CrystalReportViewer ID="crViewer" runat="server"
            AutoDataBind="True" EnableDatabaseLogonPrompt="true"
            Height="1202px"
            ToolPanelView="None" ToolPanelWidth="200px" Width="903px"  />            
        </div>
    </form>
</body>
</html>
