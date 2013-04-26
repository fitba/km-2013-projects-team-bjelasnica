<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="FITKMS.QA.Tags" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="dashboard-wrapper">
        <div class="main-container">


            <div class="row-fluid">
          
                 <div class="widget">
                                            <div class="widget-header">
                                                <div class="title">
                                                    <span class="fs1" aria-hidden="true" data-icon="&#xe0b5;"></span> Tagovi
                 
                                                </div>
                                            </div>
                                            
    <asp:DataList ID="dtListaTagova" runat="server" RepeatDirection="Horizontal">
        <ItemTemplate>
            <div style="margin:5px;">
                <div style="height:27px; background-color:#d3d0d0; font-size:14px; width:145px; padding-top:7px; padding-left:5px;">
                    <a href='TagDetaljno.aspx?id=<%# Eval("TagID") %>'><%# Eval("Naziv") %></a>
                    
                </div>
                <div style="height:145px; width:140px; background-color:#e8e6e6; padding:5px; ">
                    <a href='TagDetaljno.aspx?id=<%# Eval("TagID") %>'><%# Eval("Opis") %></a>
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>                                       
                                        </div>
    </div>
      </div>
    </div>
















</asp:Content>
