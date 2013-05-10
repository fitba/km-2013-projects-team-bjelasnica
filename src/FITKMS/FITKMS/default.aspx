<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FITKMS.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget">
        <div class="widget-body">
            <div id="tab" class="btn-group" data-toggle="buttons-radio">
                <a href="#wiki" class="btn active" data-toggle="tab">WIKI</a>
                <a href="#questions" class="btn" data-toggle="tab">PITANJA</a>
                <a href="#comments" class="btn" data-toggle="tab">KOMENTARISANO</a>
                <a href="#answers" class="btn hidden-phone" data-toggle="tab">ODGOVORENO</a>
                 <a href="#unanswered" class="btn hidden-phone" data-toggle="tab">NEODGOVORENO</a>
            </div>
            <div class="tab-content">
                <div class="tab-pane active" id="wiki">
                    <asp:DataGrid ID="articlesGrid" runat="server" AutoGenerateColumns="false" DataSource="<%# articles %>" ShowHeader="false"
                        DataKeyField="ClanakID" GridLines="None" OnItemDataBound="articlesGrid_ItemDataBound">
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <strong>
                                        <asp:LinkButton ID="titleLink" runat="server" PostBackUrl='<%# string.Format("Wiki/Details.aspx?articleId={0}", Eval("ClanakID")) %>' Text='<%# Eval("Naslov") %>' Font-Size="Medium"></asp:LinkButton>
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
                                            PostBackUrl='<%# string.Format("Wiki/Details.aspx?comments=1&articleId={0}", Eval("ClanakID")) %>'></asp:LinkButton>
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
                <div class="tab-pane" id="questions">
                    <asp:DataGrid ID="questionsGrid" runat="server" AutoGenerateColumns="false" Width="100%" DataSource="<%# questions %>" ShowHeader="false"
                        DataKeyField="PitanjeID" GridLines="None" OnItemDataBound="questionsGrid_ItemDataBound">
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <a href='QA/Details.aspx?id=<%#Eval("PitanjeID")%>' style="font-size: 16px;"><strong><%# Eval("Naslov") %></strong> </a>
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
                                        <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl='<%# string.Format("QA/Details.aspx?id={0}", Eval("PitanjeID")) %>' Text='<%# string.Format("{0} Odgovora", Eval("BrojOdgovora")) %>'></asp:LinkButton>
                                        |  <span class="fs1" aria-hidden="true" data-icon="&#xe07e;"></span>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("BrojPregleda") %>'></asp:LinkButton>
                                        &nbsp;<span class="fs1" aria-hidden="true" data-icon="&#xe0d4;"></span>
                                        <asp:LinkButton ID="LinkButton4" runat="server" Text='<%# string.Format("{0}", Eval("Pozitivni")) %>'></asp:LinkButton>
                                        &nbsp; <span class="fs1" aria-hidden="true" data-icon="&#xe0d5;"></span>
                                        <asp:LinkButton ID="LinkButton5" runat="server" Text='<%# string.Format("{0}", Eval("Negativni")) %>'></asp:LinkButton>
                                        | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tags:
                                                    <asp:Repeater ID="tagsRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-success" PostBackUrl='<%# string.Format("Tags/Details.aspx?id={0}", Eval("TagID")) %>' Text='<%# Eval("Naziv") %>'></asp:LinkButton>
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
                <div class="tab-pane" id="comments">
                    <asp:DataGrid ID="commentsArticleGrid" runat="server" AutoGenerateColumns="false" DataSource="<%# commentsArticles %>" ShowHeader="false"
                        DataKeyField="ClanakID" GridLines="None" OnItemDataBound="commentsArticleGrid_ItemDataBound">
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <strong>
                                        <asp:LinkButton ID="titleLink" runat="server" PostBackUrl='<%# string.Format("Wiki/Details.aspx?comments=1&articleId={0}", Eval("ClanakID")) %>' Text='<%# Eval("Naslov") %>' Font-Size="Medium"></asp:LinkButton>
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
                                            PostBackUrl='<%# string.Format("Wiki/Details.aspx?comments=1&articleId={0}", Eval("ClanakID")) %>'></asp:LinkButton>
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
                <div class="tab-pane" id="answers">
                    <asp:DataGrid ID="answersQuestionGrid" runat="server" AutoGenerateColumns="false" Width="100%" DataSource="<%# answerQuestions %>" ShowHeader="false"
                        DataKeyField="PitanjeID" GridLines="None" OnItemDataBound="answersQuestionGrid_ItemDataBound">
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <a href='QA/Details.aspx?id=<%#Eval("PitanjeID")%>' style="font-size: 16px;"><strong><%# Eval("Naslov") %></strong> </a>
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
                                        <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl='<%# string.Format("QA/Details.aspx?id={0}", Eval("PitanjeID")) %>' Text='<%# string.Format("{0} Odgovora", Eval("BrojOdgovora")) %>'></asp:LinkButton>
                                        |  <span class="fs1" aria-hidden="true" data-icon="&#xe07e;"></span>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("BrojPregleda") %>'></asp:LinkButton>
                                        &nbsp;<span class="fs1" aria-hidden="true" data-icon="&#xe0d4;"></span>
                                        <asp:LinkButton ID="LinkButton4" runat="server" Text='<%# string.Format("{0}", Eval("Pozitivni")) %>'></asp:LinkButton>
                                        &nbsp; <span class="fs1" aria-hidden="true" data-icon="&#xe0d5;"></span>
                                        <asp:LinkButton ID="LinkButton5" runat="server" Text='<%# string.Format("{0}", Eval("Negativni")) %>'></asp:LinkButton>
                                        | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tags:
                                                    <asp:Repeater ID="tagsRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-success" PostBackUrl='<%# string.Format("Tags/Details.aspx?id={0}", Eval("TagID")) %>' Text='<%# Eval("Naziv") %>'></asp:LinkButton>
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
                 <div class="tab-pane" id="unanswered">
                    <asp:DataGrid ID="unansweredGrid" runat="server" AutoGenerateColumns="false" Width="100%" DataSource="<%# unansweredQuestions %>" ShowHeader="false"
                        DataKeyField="PitanjeID" GridLines="None" OnItemDataBound="answersQuestionGrid_ItemDataBound">
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <a href='QA/Details.aspx?id=<%#Eval("PitanjeID")%>' style="font-size: 16px;"><strong><%# Eval("Naslov") %></strong> </a>
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
                                        |  <span class="fs1" aria-hidden="true" data-icon="&#xe07e;"></span>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("BrojPregleda") %>'></asp:LinkButton>
                                        &nbsp;<span class="fs1" aria-hidden="true" data-icon="&#xe0d4;"></span>
                                        <asp:LinkButton ID="LinkButton4" runat="server" Text='<%# string.Format("{0}", Eval("Pozitivni")) %>'></asp:LinkButton>
                                        &nbsp; <span class="fs1" aria-hidden="true" data-icon="&#xe0d5;"></span>
                                        <asp:LinkButton ID="LinkButton5" runat="server" Text='<%# string.Format("{0}", Eval("Negativni")) %>'></asp:LinkButton>
                                        | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tags:
                                                    <asp:Repeater ID="tagsRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-success" PostBackUrl='<%# string.Format("Tags/Details.aspx?id={0}", Eval("TagID")) %>' Text='<%# Eval("Naziv") %>'></asp:LinkButton>
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
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" Visible="false" />
</asp:Content>
