<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FITKMS.Wiki.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget-body">
                        <div class="widget">
                            <div class="widget-header">
                                <div class="title">
                                   <span class="fs1" aria-hidden="true" data-icon="&#xe022;"></span> Članci
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="input-append">
                                    <asp:DropDownList ID="typesList" runat="server" DataSource="<%# types %>" DataTextField="Naziv" DataValueField="VrstaID"></asp:DropDownList>
                                    <asp:TextBox ID="searchInput" runat="server" CssClass="span6" Placeholder="Tekst pretrage"></asp:TextBox>
                                    <asp:Button ID="searchArticlesSubmit" runat="server" CssClass="btn btn-info" Text="Traži"
                                        Height="30px" OnClick="searchArticlesSubmit_Click" />
                                </div>
                                <br />
                                <asp:DataGrid ID="articlesGrid" runat="server" AutoGenerateColumns="false" Width="60%" DataSource="<%# articles %>" ShowHeader="false"
                                    DataKeyField="ClanakID" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanged="articlesGrid_PageIndexChanged" 
                                    GridLines="None" OnItemDataBound="articlesGrid_ItemDataBound" PageSize="10">
                                    <PagerStyle Mode="NumericPages" CssClass="pgr" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <strong>
                                                    <asp:LinkButton ID="titleLink" runat="server" CommandName="detailsCommand" Text='<%# Eval("Naslov") %>' Font-Size="Medium"></asp:LinkButton>
                                                </strong>
                                                <br />
                                                <div style="text-align: justify">
                                                    <asp:Literal ID="textLiteral" runat="server"></asp:Literal>
                                                </div>
                                                <p class="right-align-text">
                                                    <asp:LinkButton ID="detailsLink" runat="server" CssClass="btn btn-inverse" PostBackUrl='<%# string.Format("Details.aspx?articleId={0}", Eval("ClanakID")) %>'>Detalji</asp:LinkButton>
                                                </p>
                                                <p class="icomoon-small">
                                                    <span class="fs1" aria-hidden="true" data-icon="&#xe070;"></span>by  
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Eval("KorisnickoIme") %>'></asp:LinkButton>
                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:dd.MM.yyyy}", Eval("DatumKreiranja")) %>'></asp:Label>
                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe1c3;"></span>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" Text='<%# string.Format("{0} komentara", Eval("BrojKomentara")) %>'></asp:LinkButton>
                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tagovi:
                                                    <asp:Repeater ID="tagsRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-important" Text='<%# Eval("Naziv") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <br />
                                                </p>
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
    </div>
</asp:Content>
