<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="FITKMS.Wiki.Add" ValidateRequest="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type = "text/javascript">
         function ClientItemSelected(sender, e) {
            $get("<%=tagsInput.ClientID %>").value += ', ';
         }
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="main-container">
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
            <div class="widget no-margin">
                <div class="widget-header">
                    <div class="title">
                        <span class="fs1" aria-hidden="true" data-icon="&#xe022;"></span>
                        Novi članak
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="widget-body">
                        <div class="control-group">
                            <div class="controls controls-row">
                                <asp:DropDownList ID="typesList" runat="server" DataTextField="Naziv" DataValueField="VrstaID" DataSource="<%# types %>"></asp:DropDownList>
                                <asp:DropDownList ID="themeList" runat="server" DataTextField="Naziv" DataValueField="TemaID" DataSource="<%# themes %>"></asp:DropDownList>
                                <asp:TextBox ID="titleInput" runat="server" CssClass="input-block-level" Placeholder="Naslov" required="required" x-moz-errormessage="Obavezno unesite naslov!"></asp:TextBox>
                                <asp:TextBox ID="authorsInput" runat="server" CssClass="input-block-level" Placeholder="Autori" required="required" x-moz-errormessage="Obavezno unesite autora/e!"></asp:TextBox>
                                <asp:TextBox ID="keyWordsInput" runat="server" CssClass="input-block-level" Placeholder="Ključne riječi" required="required" x-moz-errormessage="Obavezno unesite ključne riječi!"></asp:TextBox>

                                <div class="wysiwyg-container">
                                    <textarea id="wysiwyg" class="span12" placeholder="Tekst članka" style="height: 300px" runat="server" required="required" x-moz-errormessage="Obavezno unesite tekst članka!">
                                    </textarea>
                                </div>
                                <asp:TextBox ID="tagsInput" runat="server" CssClass="input-block-level" placeholder="Unesite tagove">
                                </asp:TextBox>
                                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                                </asp:ScriptManager>
                                <ajaxToolkit:AutoCompleteExtender ServiceMethod="GetTagNames" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="False" CompletionSetCount="10" TargetControlID="tagsInput" DelimiterCharacters=", "
                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" OnClientItemSelected="ClientItemSelected" 
                                    ShowOnlyCurrentWordInCompletionListItem="True" CompletionListHighlightedItemCssClass="label label-important" CompletionListItemCssClass="label">
                                </ajaxToolkit:AutoCompleteExtender>
                              
                                <br />
                                <br />
                                <asp:FileUpload ID="documentFile" runat="server" AllowMultiple="false" required="required" x-moz-errormessage="Obavezno odaberite dokument!" />
                             
                                <div class="right-align-text">
                                    <asp:Button ID="saveArticleSubmit" runat="server" CssClass="btn btn-info" Text="Sačuvaj"
                                        Height="30px" OnClick="saveArticleSubmit_Click" OnClientClick="javascript:scroll(0,0);" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
