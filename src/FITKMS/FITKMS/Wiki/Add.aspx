﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="FITKMS.Wiki.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div class="widget no-margin">
                <div class="row-fluid">
                    <div class="widget-body">
                        <div class="control-group">
                            <div class="controls controls-row">
                                <asp:DropDownList ID="typesList" runat="server" DataTextField="Naziv" DataValueField="VrstaID" DataSource="<%# types %>"></asp:DropDownList>
                                <asp:TextBox ID="titleInput" runat="server" CssClass="input-block-level" Placeholder="Naslov"></asp:TextBox>
                                <asp:TextBox ID="authorsInput" runat="server" CssClass="input-block-level" Placeholder="Autori"></asp:TextBox>
                                <asp:TextBox ID="keyWordsInput" runat="server" CssClass="input-block-level" Placeholder="Ključne riječi"></asp:TextBox>
               
                            </div>
                        </div>
                        <div class="wysiwyg-container">
                            <textarea id="wysiwyg" class="span12" placeholder="Članak" style="height: 300px" runat="server">
                            </textarea>
                        </div>
                        <a href="#myModal" role="button" class="btn btn-warning2" data-toggle="modal">Odaberite tagove
                        </a>
                        <!-- Modal -->
                        <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    ×
                                </button>
                                <h4 id="myModalLabel">Izaberite tagove
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="squared-three pull-left">
                                    <asp:CheckBoxList ID="tagsList" runat="server" DataSource="<%# tags %>"
                                        DataTextField="Naziv" DataValueField="TagID" RepeatDirection="Horizontal" RepeatColumns="10">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="saveTagsSubmit" runat="server" CssClass="btn btn-info" Text="Sačuvaj" Height="30px" />
                            </div>
                        </div>
                        <!-- End Modal -->
                        <div class="right-align-text">
                            <asp:Button ID="priviewSubmit" runat="server" CssClass="btn btn-inverse" Text="Priview" Height="30px" />
                            <asp:Button ID="saveArticleSubmit" runat="server" CssClass="btn btn-info" Text="Sačuvaj" Height="30px" OnClick="saveArticleSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>