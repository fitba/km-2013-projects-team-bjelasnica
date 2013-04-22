<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="FITKMS.Wiki.Add" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function fireServerButtonEvent() {
            document.getElementById("loadTagsSubmit").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget-body">

                        <div id="success_label" class="alert alert-block alert-success fade in" runat="server" visible="false">
                            <button data-dismiss="alert" class="close" type="button">
                                ×
                            </button>
                            <h4 class="alert-heading">Poruka!
                            </h4>
                            <p>
                                <asp:Label ID="successLabel" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>

                        <div id="error_label" class="alert alert-block alert-error fade in" runat="server" visible="false">
                            <button data-dismiss="alert" class="close" type="button">
                                ×
                            </button>
                            <h4 class="alert-heading">Greška!
                            </h4>
                            <p>
                                <asp:Label ID="errorLabel" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="widget no-margin">
                <div class="row-fluid">
                    <div class="widget-body">
                        <div class="control-group">
                            <div class="controls controls-row">
                                <asp:DropDownList ID="typesList" runat="server" DataTextField="Naziv" DataValueField="VrstaID" DataSource="<%# types %>"></asp:DropDownList>
                                <asp:DropDownList ID="themeList" runat="server" DataTextField="Naziv" DataValueField="TemaID" DataSource="<%# themes %>"></asp:DropDownList>
                                <asp:TextBox ID="titleInput" runat="server" CssClass="input-block-level" Placeholder="Naslov" required="required" x-moz-errormessage="Obavezno unesite naslov!"></asp:TextBox>
                                <asp:TextBox ID="authorsInput" runat="server" CssClass="input-block-level" Placeholder="Autori" required="required" x-moz-errormessage="Obavezno unesite autora/e!"></asp:TextBox>
                                <asp:TextBox ID="keyWordsInput" runat="server" CssClass="input-block-level" Placeholder="Ključne riječi" required="required" x-moz-errormessage="Obavezno unesite ključne riječi!"></asp:TextBox>
                            </div>
                        </div>
                        <div class="wysiwyg-container">
                            <textarea id="wysiwyg" class="span12" placeholder="Tekst članka" style="height: 300px" runat="server" required="required" x-moz-errormessage="Obavezno unesite tekst članka!">
                            </textarea>
                        </div>
                        <asp:FileUpload ID="documentFile" runat="server" AllowMultiple="false" />
                        <br />
                        <br />
                        <asp:TextBox ID="tagsInput" runat="server" CssClass="input-block-level" ReadOnly="true"></asp:TextBox>
                        <a href="#myModal" role="button" class="btn btn-warning2" data-toggle="modal">Odaberite tagove
                        </a>
                        <asp:Button ID="loadTagsSubmit" runat="server" Style="display: none" OnClick="loadTagsSubmit_Click" CausesValidation="false" />
                        <!-- Modal -->
                        <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="fireServerButtonEvent()">
                                    ×</button>
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
                                <asp:Button ID="saveTagsSubmit" runat="server" CssClass="btn btn-info" Text="Sačuvaj" CausesValidation="false" Height="30px" OnClick="saveTagsSubmit_Click" />
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
