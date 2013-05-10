<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FITKMS.Wiki.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-body">
        <div class="widget">
            <div class="widget-header">
                <div class="title">
                    <span class="fs1" aria-hidden="true" data-icon="&#xe022;"></span>&nbsp;Članci
                </div>
                <div class="tools pull-right">
                    <asp:LinkButton ID="addLink" runat="server" Width="80px" PostBackUrl="/Wiki/Add.aspx" CssClass="btn btn-inverse">Novi članak</asp:LinkButton>
                </div>
            </div>
            <div class="widget-body">
                <div class="input-append">
                    <asp:TextBox ID="searchInput" runat="server" CssClass="span6" Placeholder="Tekst pretrage"></asp:TextBox>
                    <asp:Button ID="searchArticlesSubmit" runat="server" CssClass="btn btn-info" Text="Traži"
                        Height="30px" OnClick="searchArticlesSubmit_Click" />
                </div>
                <br />
                <asp:DataGrid ID="articlesGrid" runat="server" AutoGenerateColumns="false" DataSource="<%# articles %>" ShowHeader="false"
                    DataKeyField="ClanakID" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanged="articlesGrid_PageIndexChanged"
                    GridLines="None" OnItemDataBound="articlesGrid_ItemDataBound" PageSize="10">
                    <PagerStyle Mode="NumericPages" CssClass="pgr" />
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <strong>
                                    <asp:LinkButton ID="titleLink" runat="server" PostBackUrl='<%# string.Format("Details.aspx?articleId={0}", Eval("ClanakID")) %>' Text='<%# Eval("Naslov") %>' Font-Size="Medium"></asp:LinkButton>
                                </strong>
                                <br />
                                <div style="text-align: justify">
                                    <asp:Literal ID="textLiteral" runat="server"></asp:Literal>
                                </div>
                                <p class="icomoon-small" style="margin-top: 10px">
                                    <span class="fs1" aria-hidden="true" data-icon="&#xe070;"></span>by  
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Eval("KorisnickoIme") %>'></asp:LinkButton>
                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>
                                    <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:dd.MM.yyyy}", Eval("DatumKreiranja")) %>'></asp:Label>
                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe1c3;"></span>
                                    <asp:LinkButton ID="LinkButton3" runat="server" Text='<%# string.Format("{0} komentara", Eval("BrojKomentara")) %>'
                                        PostBackUrl='<%# string.Format("Details.aspx?comments=1&articleId={0}", Eval("ClanakID")) %>'></asp:LinkButton>
                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tagovi:
                                                    <asp:Repeater ID="tagsRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-important" Text='<%# Eval("Naziv") %>' PostBackUrl='<%# string.Format("/Tags/Details.aspx?Id={0}", Eval("TagID")) %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                    <hr />
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </div>
</asp:Content>
