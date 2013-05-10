<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FITKMS.Tags.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div class="row-fluid">
                <div class="widget">
                    <div class="widget-header">
                        <div class="title">
                            <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>&nbsp;Tagovi
                 
                        </div>
                    </div>
                    <div class="input-append" style="margin-top: 10px; margin-left: 5px;">
                        <asp:TextBox ID="searchInput" runat="server" CssClass="span4" Placeholder="Unesite naziv taga za pretragu"></asp:TextBox>
                        <asp:Button ID="searchTagsSubmit" runat="server" CssClass="btn btn-info" Text="Traži"
                            Height="30px" OnClick="searchTagsSubmit_Click" />
                    </div>
                    <asp:DataList ID="listTags" runat="server" Width="99.2%" RepeatDirection="Horizontal" RepeatColumns="6">
                        <ItemTemplate>
                            <div style="margin: 7px;">
                                <div style="height: 27px; background-color: #d3d0d0; font-size: 14px; width: 100%; padding: 5px;">
                                    <a href='Details.aspx?id=<%# Eval("TagID") %>'><span class="label label-info"> <%# Eval("Naziv") %></span> x <%# Eval("BrojPonavljanja") %></a>
                                </div>
                                <div style="height: 130px; width: 100%; background-color: #e8e6e6; padding: 5px;">
                                    <a href='Details.aspx?id=<%# Eval("TagID") %>'><%# (string) Eval("Opis").ToString().Substring(0, 100) %>...</a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Panel ID="panelPager" runat="server">
                        <div class="next-prev-btn-container" style="text-align:center; margin-bottom: 5px;">
                            <asp:LinkButton ID="linkPrevious" class="button prev" Width="70px" runat="server" OnClick="linkPrevious_Click">Prethodna</asp:LinkButton>
                            <asp:Label ID="labelCurrent" class="button" Width="125px" runat="server"></asp:Label>
                            <asp:LinkButton ID="linkNext" class="button next" Width="70px" runat="server" OnClick="linkNext_Click">Sljedeća</asp:LinkButton>
                            
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget-body">

                        <div id="warning_label" class="alert alert-block alert-warning fade in" runat="server" visible="false">
                            <button data-dismiss="alert" class="close" type="button">
                                ×
                            </button>
                            <h4 class="alert-heading">Upozorenje!
                            </h4>
                            <p>
                               <asp:Label ID="warningLabel" runat="server" Text="Label">Nisu pronađeni traženi tagovi!</asp:Label>
                            </p>
                        </div>
                   </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
