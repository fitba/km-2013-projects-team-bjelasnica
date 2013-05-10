<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="FITKMS.QA.Details" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Content/css/rating.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            $("#wysiwyg").html("");
            $("#wysiwyg").text("");
            $("#wysiwyg").val("");
        });
    </script>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="widget body">
        <div class="widget-header">
            <div class="title">
                <span class="fs1" aria-hidden="true" data-icon="&#xe0f6;"></span>
                <asp:Label ID="lblNaslovPitanja" runat="server" Font-Size="Medium"></asp:Label>
            </div>
            <div class="tools pull-right">
                    <asp:LinkButton ID="editLink" runat="server" CssClass="btn btn-inverse">Edit</asp:LinkButton>
                </div>
        </div>
        <div class="widget-body">
            <div class="row-fluid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <div id="warning_block" class="alert alert-block alert-warning fade in" runat="server" visible="false">
                            <button data-dismiss="alert" class="close" type="button">
                                ×</button>
                            <h4 class="alert-heading">Upozorenje!
                            </h4>
                            <p>
                                <asp:Label ID="warning" runat="server" Text="Label"></asp:Label>
                                <a href="/Login.aspx?ReturnUrl=/QA/Details.aspx?id=<%= Request["id"] %>" class="label label-important">Prijavi se</a>
                            </p>
                        </div>
                        <div class="span8">
                            <p class="icomoon-small">
                            <a href="#"><span class="fs1" aria-hidden="true" data-icon="&#xe070;"></span>
                                <asp:Label ID="lblKorisnik" runat="server" Text=""></asp:Label>
                            </a>
                            | <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>Kreirano
                            <asp:Label ID="lblDatum" runat="server" Text=""></asp:Label>
                            | <span class="fs1" aria-hidden="true" data-icon="&#xe07e;"></span>
                            <asp:Label ID="lblBrojPregleda" runat="server" Text=""></asp:Label>
                            | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tags :
                        <asp:Repeater ID="tagsRepeater" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-success" PostBackUrl='<%# string.Format("../Tags/Details.aspx?id={0}", Eval("TagID")) %>' Text='<%# Eval("Naziv") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                                </p>Sviđa mi se:
                            <asp:LinkButton ID="likePitanje" runat="server" OnClick="likePitanje_Click">
                            <span class="fs1" aria-hidden="true" data-icon="&#xe0d4;"></span>
                            <asp:Label ID="lblBrojPozitnivh" runat="server" Text=""></asp:Label>
                        </asp:LinkButton>
                            <asp:LinkButton ID="dislikePitanje" runat="server" OnClick="dislikePitanje_Click">
                                &nbsp;  <span class="fs1" aria-hidden="true" data-icon="&#xe0d5;"></span>
                                <asp:Label ID="lblBrojNegativnih" runat="server" Text=""></asp:Label>
                            </asp:LinkButton>
                        </div>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <hr>
            <asp:Label ID="lblTextPitanja" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <!--početak pitanja-->
    <div class="accordion-group">
        <div class="accordion-heading">
            <a href="#collapseOne" data-parent="#accordion1" data-toggle="collapse" class="accordion-toggle">
                <i class="icon-pencil icon-white"></i>
                <asp:Label ID="labelTag" runat="server" Text="Odgovori"></asp:Label>
            </a>
        </div>
    </div>
    <div style="overflow: auto; border: none; padding: 0px; background-color: #F8F8F8;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <!--Pocetak pitanja-->
                <asp:DataList ID="dtOdgovori" runat="server" Width="100%">
                    <ItemTemplate>
                        <div style="overflow: auto; border: 1px solid #C0C0C0; margin-bottom: 5px;">
                            <div style="height: 75px; margin-right: 5px; padding-left: 5px;">
                                <div style="float: left; margin-top: 1px;">
                                    <img class="avatar" alt="" src='<%# string.Format("/Users/ImageHandler.ashx?userId={0}", Eval("KorisnikID")) %>' style="width: 60px; height: 60px;">
                                </div>
                                <div class="tools pull-right" runat="server" id="deleteLinkBlock">
                                    <asp:LinkButton ID="deleteLink" runat="server" Visible="false" CommandName="deleteCommand"
                                        CommandArgument='<%# Eval("OdgovorID") %>' formnovalidate="formnovalidate" CssClass="label label-info" OnClick="deleteLink_Click">Ukloni</asp:LinkButton>
                                </div>
                                <div style="margin: 5px 0px 0px 70px; padding-top: 5px;">
                                    <div style="width: 20%; float: left;">
                                        <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>
                                        <asp:Label ID="dateLabel" runat="server" Text='<%# string.Format("{0:dd.MM.yyyy HH:mm}", Eval("Datum")) %>'></asp:Label></span>
                                    </div>
                                    <label style="margin: 25px 0px 6px 0px; font-weight: bold; font-size: 14px;"><%# Eval("Ime") %> <%# Eval("Prezime") %> </label>
                                </div>
                            </div>
                            <div class="message" style="margin: -10px 5px 0px 5px;">
                                <span class="body"><%# Eval("Tekst") %></span>
                            </div>
                            <div class="pull-right" style="margin: 5px;">
                                <asp:LinkButton ID="likeButton" runat="server" CommandArgument='<%# Eval("OdgovorID") %>' OnClick="likeButton_Click">
                                                                <span class="fs1" aria-hidden="true" data-icon="&#xe0d4;" style="margin-right:3px;"></span><%# Eval("Pozitivni") %> Like 
                                </asp:LinkButton>
                                <asp:LinkButton ID="dislikeButton" runat="server" CommandArgument='<%# Eval("OdgovorID") %>' OnClick="dislikeButton_Click"> 
                                                                        <span class="fs1" aria-hidden="true" data-icon="&#xe0d5;" style="margin-right:3px;"></span><%# Eval("Negativni") %> Dislike 
                                </asp:LinkButton>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <!--Kraj pitanja-->
        <asp:Panel ID="answerPanel" runat="server" Visible="false">
            <div class="wysiwyg-container">
                <asp:TextBox runat="server" ID="wysiwyg" placeholder="Unesite odgovor pitanje ..." Style="height: 115px; width: 98%;"></asp:TextBox>
            </div>
            <div class="right-align-text" style="margin: 0 5px 5px 5px">
                <asp:Button ID="Button1" runat="server" Text="Sačuvaj" class="btn btn-info" OnClick="Button1_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="answerMessagePanel" runat="server" Visible="false">
            <div style="border: 1px solid #C0C0C0;">
                <div style="margin: 5px">
                    <img class="avatar" alt="" src="../Content/img/profile.png" style="width: 60px; height: 60px;" />
                    <div class="message">
                        <span class="arrow"></span>
                        <span class="body">Da biste mogli odgovoriti na pitanje, potrebno je da budete registrovani. 
                                                            Ukoliko ste već registrovani, prijavite se na sistem.
                                                            <br />
                            <br />
                            <a href="/Login.aspx?ReturnUrl=/QA/Details.aspx?id=<%= Request["id"] %>" class="label label-important">Prijavi se</a>
                            <a href="../Users/Registration.aspx" class="label label-important">Registracija</a>
                        </span>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
