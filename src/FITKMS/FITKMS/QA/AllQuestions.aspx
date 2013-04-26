<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AllQuestions.aspx.cs" Inherits="FITKMS.QA.AllQuestions" %>
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
                                            
    <asp:DataList ID="dtListaPitanja" runat="server" >
        <ItemTemplate>
            <div style="margin:5px;">
                <div style="height:27px; background-color:#d3d0d0; font-size:14px;  padding-top:7px; padding-left:5px; width:100%;">
                    <a href='Questions.aspx?id=<%# Eval("PitanjeID") %>'><%# Eval("Naslov") %></a>
                    
                </div>
                <div style="height:27px; background-color:#d3d0d0; font-size:14px;  padding-top:7px; padding-left:5px;width:100%;">
                    <a href='Questions.aspx?id=<%# Eval("PitanjeID") %>'><%# Eval("Tekst") %></a>
                    
                </div>
             <div style="height:27px; background-color:#d3d0d0; font-size:14px;  padding-top:7px; padding-left:5px;width:100%;">
                    lista tagova
                    
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>                                       
                                        </div>
    </div>
      </div>
    </div>

</asp:Content>
