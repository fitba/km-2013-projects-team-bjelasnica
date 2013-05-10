<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FITKMS.Themes.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-body">
        <div class="widget">
            <div class="widget-header">
                <div class="title">
                    <span class="fs1" aria-hidden="true" data-icon="&#xe022;"></span>&nbsp;Teme
                </div>
            </div>
            <div class="widget-body">
                <div class="input-append">
                    <asp:TextBox ID="searchInput" runat="server" CssClass="span6" Placeholder="Unesite temu"></asp:TextBox>
                    <asp:Button ID="searchSubmit" runat="server" CssClass="btn btn-info" Text="Traži"
                        Height="30px" OnClick="searchSubmit_Click" />

                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                    </asp:ScriptManager>
                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="GetThemeNames" MinimumPrefixLength="3"
                        CompletionInterval="100" EnableCaching="False" CompletionSetCount="10" TargetControlID="searchInput"
                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false"
                        ShowOnlyCurrentWordInCompletionListItem="True" CompletionListHighlightedItemCssClass="label label-important" CompletionListItemCssClass="label">
                    </ajaxToolkit:AutoCompleteExtender>
                </div>
                <hr />
                <ul class="nav nav-tabs no-margin myTabBeauty">
                    <li class="active" runat="server" id="articlesTab">
                        <a data-toggle="tab" href="#articles">Članci
                        </a>
                    </li>
                    <li class="" runat="server" id="questionsTab">
                        <a data-toggle="tab" href="#questions">Pitanja
                        </a>
                    </li>
                </ul>

                <div class="tab-content" id="myTabContent">
                    <div id="articles" class="tab-pane fade active in" runat="server">
                        <asp:DataGrid ID="articlesGrid" runat="server" AutoGenerateColumns="false" DataSource="<%# articlesList %>" ShowHeader="false"
                            DataKeyField="ClanakID" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanged="articlesGrid_PageIndexChanged"
                            GridLines="None" OnItemDataBound="articlesGrid_ItemDataBound" PageSize="10">
                            <PagerStyle Mode="NumericPages" CssClass="pgr" />
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <strong>
                                            <asp:LinkButton ID="titleLink" runat="server" PostBackUrl='<%# string.Format("/Wiki/Details.aspx?articleId={0}", Eval("ClanakID")) %>' Text='<%# Eval("Naslov") %>' Font-Size="Medium"></asp:LinkButton>
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
                    <div id="questions" class="tab-pane fade">
                        <asp:DataGrid ID="questionsGrid" runat="server" AutoGenerateColumns="false" Width="100%" DataSource="<%# questions %>" ShowHeader="false"
                            DataKeyField="PitanjeID" AllowPaging="true" AllowCustomPaging="true"
                            GridLines="None" PageSize="10" OnItemDataBound="pitanjaGrid_ItemDataBound" OnPageIndexChanged="pitanjaGrid_PageIndexChanged">
                            <PagerStyle Mode="NumericPages" CssClass="pgr" />
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <a href='/QA/Details.aspx?id=<%#Eval("PitanjeID")%>' style="font-size: 16px;"><strong><%# Eval("Naslov") %></strong> </a>
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
        </div>
    </div>
</asp:Content>
