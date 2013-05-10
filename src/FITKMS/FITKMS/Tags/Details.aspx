<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="FITKMS.Tags.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div class="row-fluid">
                <div class="accordion-group">
                    <div class="accordion-heading">
                        <div class="tools pull-right" style="margin:3px;">
                            <asp:LinkButton ID="favoriteTag" runat="server" Width="70px" class="btn btn-warning2" Visible="false" OnClick="favoriteTag_Click">Favorite</asp:LinkButton>
                            <asp:LinkButton ID="noFavoriteTag" runat="server" Width="70px" class="btn btn-danger" Visible="false" OnClick="noFavoriteTag_Click">No favorite</asp:LinkButton>
                        </div>
                        <a href="#collapseOne" data-parent="#accordion1" data-toggle="collapse" class="accordion-toggle">
                            <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>
                            <asp:Label ID="labelTag" runat="server" Text=""></asp:Label>
                        </a>
                    </div>
                    <div class="accordion-body in collapse" id="collapseOne" style="height: auto;">
                        <div class="accordion-inner">
                            <asp:Label ID="labelDescription" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid" style="margin-top: 10px">
                <div class="span6">
                    <div class="widget">
                        <div class="widget-header">
                            <div class="title">
                                <span class="fs1" aria-hidden="true" data-icon="&#xe022;"></span>&nbsp;Članci
                            </div>
                        </div>
                        <div class="widget-body">
                            <asp:DataGrid ID="articlesGrid" runat="server" AutoGenerateColumns="false" DataSource="<%# articles %>" ShowHeader="false"
                                DataKeyField="ClanakID" AllowPaging="true" AllowCustomPaging="true"
                                GridLines="None" PageSize="10" OnItemDataBound="articlesGrid_ItemDataBound" OnPageIndexChanged="articlesGrid_PageIndexChanged">
                                <PagerStyle Mode="NumericPages" CssClass="pgr" />
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <strong>
                                                <asp:LinkButton ID="titleLink" runat="server" CommandName="detailsCommand" PostBackUrl='<%# string.Format("../Wiki/Details.aspx?articleId={0}", Eval("ClanakID")) %>' Text='<%# Eval("Naslov") %>' Font-Size="Medium"></asp:LinkButton>
                                            </strong>
                                            <br />
                                            <div style="text-align: justify">
                                                <asp:Literal ID="textLiteral" runat="server"></asp:Literal>
                                            </div>
                                            <p class="icomoon-small">
                                                <span class="fs1" aria-hidden="true" data-icon="&#xe070;"></span>by  
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Eval("KorisnickoIme") %>'></asp:LinkButton>
                                                | <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>
                                                <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:dd.MM.yyyy}", Eval("DatumKreiranja")) %>'></asp:Label>
                                                | <span class="fs1" aria-hidden="true" data-icon="&#xe1c3;"></span>
                                                <asp:LinkButton ID="LinkButton3" runat="server" Text='<%# string.Format("{0} komentara", Eval("BrojKomentara")) %>'></asp:LinkButton>
                                                | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tags:
                                                    <asp:Repeater ID="tagsRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-important" PostBackUrl='<%# string.Format("Details.aspx?id={0}", Eval("TagID")) %>' Text='<%# Eval("Naziv") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                <br />
                                            </p>
                                            <hr>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="widget">
                        <div class="widget-header">
                            <div class="title">
                                <span class="fs1" aria-hidden="true" data-icon="&#xe0f6;"></span>&nbsp;Pitanja
                            </div>
                        </div>
                        <div class="widget-body">
                            <asp:DataGrid ID="questionsGrid" runat="server" AutoGenerateColumns="false" DataSource="<%# questions %>" ShowHeader="false"
                                DataKeyField="PitanjeID" AllowPaging="true" AllowCustomPaging="true"
                                GridLines="None" PageSize="10" OnItemDataBound="questionsGrid_ItemDataBound" OnPageIndexChanged="questionsGrid_PageIndexChanged">
                                <PagerStyle Mode="NumericPages" CssClass="pgr" />
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <strong>
                                                <asp:LinkButton ID="titleLink" runat="server" CommandName="detailsCommand" PostBackUrl='<%# string.Format("../QA/Details.aspx?id={0}", Eval("PitanjeID")) %>' Text='<%# Eval("Naslov") %>' Font-Size="Medium"></asp:LinkButton>
                                            </strong>
                                            <br />
                                            <div style="text-align: justify">
                                                <asp:Literal ID="textLiteral" runat="server"></asp:Literal>
                                            </div>
                                            <p class="icomoon-small">
                                                <span class="fs1" aria-hidden="true" data-icon="&#xe070;"></span>by  
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Eval("KorisnickoIme") %>'></asp:LinkButton>
                                                | <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>
                                                <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:dd.MM.yyyy}", Eval("DatumKreiranja")) %>'></asp:Label>
                                                | <span class="fs1" aria-hidden="true" data-icon="&#xe006;"></span>
                                                <asp:LinkButton ID="LinkButton3" runat="server" Text='<%# string.Format("{0} odgovora", Eval("BrojOdgovora")) %>'></asp:LinkButton>
                                                | <span class="fs1" aria-hidden="true" data-icon="&#xe0d4;"></span>
                                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# string.Format("{0}", Eval("Pozitivni")) %>'></asp:LinkButton>
                                                &nbsp; <span class="fs1" aria-hidden="true" data-icon="&#xe0d5;"></span>
                                                <asp:LinkButton ID="LinkButton4" runat="server" Text='<%# string.Format("{0}", Eval("Negativni")) %>'></asp:LinkButton>
                                                | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tags:
                                                    <asp:Repeater ID="tagsRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-success" PostBackUrl='<%# string.Format("Details.aspx?id={0}", Eval("TagID")) %>' Text='<%# Eval("Naziv") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                <br />
                                            </p>
                                            <hr>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
