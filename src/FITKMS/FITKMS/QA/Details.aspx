﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="FITKMS.QA.Details" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div class="row-fluid">
                <div class="span12">
                    <div style="border: none;">
                        <div class="container-fluid">
                            <div class="row-fluid">
                                <div class="span8">
                                    <div class="widget no-margin">
                                        <div class="widget-header">
                                            <div class="title" style="font-size: 16px;">
                                                <a style="font-weight: bold;">
                                                    <asp:Label ID="lblNaslovPitanja" runat="server" Text="Label"></asp:Label></a>
                                            </div>
                                        </div>
                                        <div class="widget-body">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">
                                                    </div>
                                                    <div class="row-fluid">

                                                        <div class="span12" style="margin-bottom: 10px;">

                                                            <asp:Label ID="lblTextPitanja" runat="server" Text=""></asp:Label>

                                                        </div>
                                                    </div>
                                                    <div class="row-fluid">
                                                        <div class="span12">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                                <ContentTemplate>
                                                                    <a href="#"><span class="fs1" aria-hidden="true" data-icon="&#xe070;"></span>
                                                                        <asp:Label ID="lblKorisnik" runat="server" Text=""></asp:Label>
                                                                    </a>
                                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>
                                                                    <asp:Label ID="lblDatum" runat="server" Text=""></asp:Label>
                                                                    <asp:LinkButton ID="likePitanje" runat="server" OnClick="likePitanje_Click">
                                                                        |  <span class="fs1" aria-hidden="true" data-icon="&#xe0d4;"></span>
                                                                        <asp:Label ID="lblBrojPozitnivh" runat="server" Text=""></asp:Label>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="dislikePitanje" runat="server" OnClick="dislikePitanje_Click">
                                                                        |  <span class="fs1" aria-hidden="true" data-icon="&#xe0d5;"></span>
                                                                        <asp:Label ID="lblBrojNegativnih" runat="server" Text=""></asp:Label>
                                                                    </asp:LinkButton>
                                                                    | 
                                                                <span class="fs1" aria-hidden="true" data-icon="&#xe07e;"></span>
                                                                    <asp:Label ID="lblBrojPregleda" runat="server" Text=""></asp:Label>
                                                                    |
                                                                <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tags :
                                                                  <asp:Repeater ID="tagsRepeater" runat="server">
                                                                      <ItemTemplate>
                                                                          <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-info" Text='<%# Eval("Naziv") %>'></asp:LinkButton>
                                                                      </ItemTemplate>
                                                                  </asp:Repeater>

                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--vkraj pitnj-->
                                    <br />
                                    <!--početak pitanja-->
                                    <div class="widget no-margin" style="border: none;">
                                        <div class="widget-header" style="border: none; margin-bottom: 1px;">
                                            <div class="title" style="font-size: 16px; color: #1d4f79; font-weight: bold;">
                                                Odgvovori na pitanje
                 
                                            </div>
                                        </div>
                                        <div style="overflow: auto; border: none; padding: 0px; background-color: #F8F8F8;">

                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <!--Pocetak pitanja-->
                                                            <asp:DataList ID="dtOdgovori" runat="server" Width="100%">
                                                                <ItemTemplate>
                                                                    <div style="overflow: auto; padding-bottom: 10px; border: 1px solid #C0C0C0; margin-bottom: 10px;">
                                                                        <div style="height: 75px; margin-right: 8px; padding-left: 7px;">
                                                                            <div>
                                                                                <div style="float: left; margin-top: 2px;">
                                                                                    <img class="avatar" alt="" src="img/profile.jpg" width="70" height="70">
                                                                                </div>
                                                                                <div style="margin: 5px 0px 0px 90px; padding-top: 18px;">
                                                                                    <div style="width: 15%; float: left;">
                                                                                        <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>
                                                                                        <%# Eval("Datum") %>
                                                                                    </div>
                                                                                    <div style="width: 12%; float: left;">
                                                                                        <a href="#"><span class="fs1" aria-hidden="true" data-icon="&#xe007;" style="margin-right: 3px;"></span>Pitanja <%# Eval("UkupnoPitanja") %> </a>
                                                                                    </div>
                                                                                    <div style="width: 15%; float: left; margin-left: 15px;">
                                                                                        <a href="#"><span class="fs1" aria-hidden="true" data-icon="&#xe006;" style="margin-right: 3px;"></span>Odgovora <%# Eval("UkupnoOdgovora") %> </a>
                                                                                    </div>
                                                                                    <label style="margin: 25px 0px 6px 0px; font-weight: bold; font-size: 14px;"><%# Eval("Ime") %> <%# Eval("Prezime") %> </label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="message" style="padding: 3px 8px;">
                                                                            <span class="body"><%# Eval("Tekst") %></span>
                                                                        </div>
                                                                        <div style="margin-top: 5px;">
                                                                            <div style="width: 12%; float: left; margin-left: 8px;">


                                                                                <asp:LinkButton ID="likeButton" runat="server" CommandArgument='<%# Eval("OdgovorID") %>' OnClick="likeButton_Click">
                                                                <span class="fs1" aria-hidden="true" data-icon="&#xe0d4;" style="margin-right:3px;"></span><%# Eval("Pozitivni") %> Like 
                                                                                </asp:LinkButton>

                                                                            </div>
                                                                            <div style="width: 12%; float: left;">

                                                                                <asp:LinkButton ID="dislikeButton" runat="server" CommandArgument='<%# Eval("OdgovorID") %>' OnClick="dislikeButton_Click"> 
                                                                        <span class="fs1" aria-hidden="true" data-icon="&#xe0d5;" style="margin-right:3px;"></span><%# Eval("Negativni") %> Dislike 
                                                                                </asp:LinkButton>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </ContentTemplate>

                                                    </asp:UpdatePanel>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <!--Kraj pitanja-->
                                            <div class="wysiwyg-container">

                                                <asp:TextBox runat="server" ID="wysiwyg" placeholder="Unesite odgovor pitanje ..." Style="height: 115px; width: 98%;"></asp:TextBox>

                                            </div>

                                            <asp:Button ID="Button1" runat="server" Text="Snimi odgovor" class="btn btn-info" OnClick="Button1_Click" />

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