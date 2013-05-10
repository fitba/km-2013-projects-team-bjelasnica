<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FITKMS.QA.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-body">
        <div class="widget">
            <div class="widget-header">
                <div class="title">
                    <span class="fs1" aria-hidden="true" data-icon="&#xe0f6;"></span>&nbsp;Pitanja
                </div>
                <div class="tools pull-right">
                    <asp:LinkButton ID="addLink" runat="server" Width="80px" PostBackUrl="/QA/Add.aspx" CssClass="btn btn-inverse">Novo pitanje</asp:LinkButton>
                </div>
            </div>
            <div class="widget-body">
                <div class="input-append">
                    <asp:TextBox ID="searchInput" runat="server" CssClass="span6" Placeholder="Tekst pretrage"></asp:TextBox>
                    <asp:Button ID="searchArticlesSubmit" runat="server" CssClass="btn btn-info" Text="Traži"
                        Height="30px" OnClick="searchArticlesSubmit_Click" />
                </div>
                <br />
                <asp:DataGrid ID="pitanjaGrid" runat="server" AutoGenerateColumns="false" Width="100%" DataSource="<%# pitanja %>" ShowHeader="false"
                    DataKeyField="PitanjeID" AllowPaging="true" AllowCustomPaging="true"
                    GridLines="None" PageSize="10" OnItemDataBound="pitanjaGrid_ItemDataBound" OnPageIndexChanged="pitanjaGrid_PageIndexChanged">
                    <PagerStyle Mode="NumericPages" CssClass="pgr" />
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <strong>
                                    <asp:LinkButton ID="titleLink" runat="server" PostBackUrl='<%# string.Format("Details.aspx?id={0}", Eval("PitanjeID")) %>' Text='<%# Eval("Naslov") %>' Font-Size="Medium"></asp:LinkButton>
                                </strong>
                                <br />
                                <div style="text-align: justify; margin-top: 5px;">
                                    <asp:Literal ID="textLiteral" runat="server"></asp:Literal>
                                </div>

                                <p class="icomoon-small" style="margin-top: 10px;">
                                    <span class="fs1" aria-hidden="true" data-icon="&#xe070;"></span>by   
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Eval("KorisnickoIme") %>'></asp:LinkButton>
                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>
                                    <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:dd.MM.yyyy}", Eval("DatumKreiranja")) %>'></asp:Label>
                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe006;"></span>
                                    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl='<%# string.Format("Details.aspx?id={0}", Eval("PitanjeID")) %>' Text='<%# string.Format("{0} Odgovora", Eval("BrojOdgovora")) %>'></asp:LinkButton>
                                    |  <span class="fs1" aria-hidden="true" data-icon="&#xe07e;"></span>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("BrojPregleda") %>'></asp:LinkButton>
                                    &nbsp;<span class="fs1" aria-hidden="true" data-icon="&#xe0d4;"></span>
                                    <asp:LinkButton ID="LinkButton4" runat="server" Text='<%# string.Format("{0}", Eval("Pozitivni")) %>'></asp:LinkButton>
                                    &nbsp; <span class="fs1" aria-hidden="true" data-icon="&#xe0d5;"></span>
                                    <asp:LinkButton ID="LinkButton5" runat="server" Text='<%# string.Format("{0}", Eval("Negativni")) %>'></asp:LinkButton>
                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tags:
                                                    <asp:Repeater ID="tagsRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-success" PostBackUrl='<%# string.Format("../Tags/Details.aspx?id={0}", Eval("TagID")) %>' Text='<%# Eval("Naziv") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                    <br />
                                </p>
                                <hr />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </div>
</asp:Content>
