<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs"
    Inherits="FITKMS.Wiki.Details" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Content/css/rating.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget-body">
                        <div class="widget">
                            <div class="widget-header">
                                <div class="title">
                                    <span class="fs1" aria-hidden="true" data-icon="&#xe022;"></span>
                                    <asp:Label ID="titleLabel" runat="server" Font-Size="Medium"></asp:Label>
                                    <span class="mini-title">
                                        <asp:Label ID="typeLable" runat="server" Text="Label"></asp:Label>
                                    </span>
                                </div>
                            </div>
                            <div class="widget-body">
                                <ul class="nav nav-tabs no-margin myTabBeauty">
                                    <li class="active" runat="server" id="defaultTab">
                                        <a data-toggle="tab" href="#home">Detalji članka
                                        </a>
                                    </li>
                                    <li class="" runat="server" id="commentTab">
                                        <a data-toggle="tab" href="#articleComments">Komentari
                                        </a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="myTabContent">
                                    <div id="home" class="tab-pane fade active in" runat="server">

                                        <div id="warning_block" class="alert alert-block alert-warning fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close" type="button">
                                                ×</button>
                                            <h4 class="alert-heading">Upozorenje!
                                            </h4>
                                            <p>
                                                <asp:Label ID="warning" runat="server" Text="Label"></asp:Label>
                                                <a href="../Login.aspx" class="label label-important">Prijavi se</a>
                                            </p>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="span9">
                                                <p class="icomoon-small">
                                                    <span class="fs1" aria-hidden="true" data-icon="&#xe070;"></span>Autor:  
                                                 <asp:Label ID="authorsLabel" runat="server"></asp:Label>
                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>Kreirano:
                                            <asp:Label ID="dateCreatedLabel" runat="server"></asp:Label>
                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>Modifikovano:
                                            <asp:Label ID="dateChangedLabel" runat="server"></asp:Label>
                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe097;"></span>Prosječna ocjena:
                                                 <asp:Label ID="avgGradeLabel" runat="server"></asp:Label>
                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tagovi:
                                                    <asp:Repeater ID="tagsRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-important" Text='<%# Eval("Naziv") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </p>
                                                <strong>Tema:</strong>
                                                <asp:Label ID="themeLabel" runat="server" Text="Label"></asp:Label>

                                            </div>
                                            <div class="span3">
                                                <div class="pull-right">
                                                    <span class="label label-info" runat="server" id="rating_block" visible="false">
                                                        <asp:Label ID="ratingLabel" runat="server" Text=""></asp:Label>
                                                        | <span class="fs1" aria-hidden="true" data-icon="&#xe052;">Datum: 
                                                        <asp:Label ID="dateRatedLabel" runat="server" Text=""> </asp:Label>
                                                        </span>
                                                    </span>
                                                    <ajaxToolkit:Rating ID="articleRating" runat="server" MaxRating="5" CurrentRating="0" CssClass="ratingStar"
                                                        StarCssClass="ratingItem" WaitingStarCssClass="Saved" FilledStarCssClass="Filled"
                                                        EmptyStarCssClass="Empty" AutoPostBack="true" OnChanged="articleRating_Changed">
                                                    </ajaxToolkit:Rating>
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <p>
                                            <asp:Literal ID="textLiteral" runat="server"></asp:Literal>
                                        </p>
                                        <div class="doc-icons-container" id="download_block">
                                            <div class="icon light-blue hidden-tablet" runat="server" id="pdfIcon" visible="false">
                                                <a runat="server" id="pdfDownloadLink" href="#" target="_blank">
                                                    <span class="fs1 doc-icon" aria-hidden="true" data-icon="&#xe1b2;"></span>
                                                    <span class="doc-type">PDF
                                                    </span>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="articleComments" class="tab-pane fade" runat="server">
                                        <div class="span8">
                                            <asp:DataGrid ID="commentsGrid" runat="server" DataSource="<%# comments %>" AutoGenerateColumns="false"
                                                GridLines="None" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanged="commentsGrid_PageIndexChanged" PageSize="3" Width="100%">
                                                <PagerStyle Mode="NumericPages" CssClass="pgr" Position="Top" />
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <ul class="comments">
                                                                <li id="commentBlock" class="in" runat="server">
                                                                    <img class="avatar" alt="" src='<%# string.Format("/Users/ImageHandler.ashx?userId={0}", Eval("KorisnikID")) %>' />
                                                                    <div class="message">
                                                                        <span class="arrow"></span>
                                                                        <asp:LinkButton ID="nameLink" runat="server" CssClass="name" Text='<%# Eval("Korisnik") %>'></asp:LinkButton>
                                                                        <span class="fs1" aria-hidden="true" data-icon="&#xe052;">
                                                                            <span class="date-time">
                                                                                <asp:Label ID="dateLabel" runat="server" Text='<%# string.Format("{0:dd.MM.yyyy HH:mm}", Eval("DatumKreiranja")) %>'></asp:Label></span>
                                                                            <span class="body">
                                                                                <asp:Literal ID="commentLiteral" runat="server" Text='<%# Eval("Komentar") %>'></asp:Literal>
                                                                            </span>
                                                                    </div>
                                                                    <br />
                                                                    <br />
                                                                </li>
                                                            </ul>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            <div class="widget-body">
                                                <div id="warning_label" class="alert alert-block alert-warning fade in" runat="server" visible="false">
                                                    <button data-dismiss="alert" class="close" type="button">
                                                        ×</button>
                                                    <h4 class="alert-heading">Upozorenje!
                                                    </h4>
                                                    <p>
                                                        <asp:Label ID="warningLabel" runat="server" Text="Label"></asp:Label>
                                                        <a href="../Login.aspx" class="label label-important">Prijavi se</a>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="right-align-text">
                                                <asp:Button ID="newCommentSubmit" runat="server" Text="Dodaj komentar" CssClass="btn btn-inverse" Height="30px" OnClick="newCommentSubmit_Click"
                                                    OnClientClick="javascript:scroll(0, document.body.scrollHeight);" />
                                            </div>
                                            <asp:Panel ID="commentPanel" runat="server" Visible="false">
                                                <hr />
                                                <div class="wysiwyg-container">
                                                    <textarea id="wysiwyg" class="span12" placeholder="Unesite komentar" style="height: 200px" runat="server" required="required" x-moz-errormessage="Obavezno unesite komentar!">
                                                    </textarea>
                                                </div>
                                                <div class="right-align-text">
                                                    <asp:Button ID="addCommentSubmit" runat="server" CssClass="btn btn-info" Text="Sačuvaj"
                                                        Height="30px" OnClick="addCommentSubmit_Click" OnClientClick="javascript:scroll(0,0);" />
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
