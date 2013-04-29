<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="FITKMS.Users.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <div class="row-fluid">
                <div class="span6">
                    <div class="widget">
                        <div class="widget-header">
                            <div class="title">
                                <span class="fs1" aria-hidden="true" data-icon="&#xe022;"></span>&nbsp;Registracija
                            </div>
                        </div>
                        <div class="widget-body">
                            <div class="form-horizontal no-margin">
                                <div class="control-group">
                                    <label class="control-label">
                                        Ime i prezime
                                    </label>
                                    <div class="controls controls-row">
                                        <asp:TextBox ID="fnameInput" class="span6" runat="server" required="required" placeholder="Ime"></asp:TextBox>
                                        <asp:TextBox ID="lnameInput" class="span6 input-left-top-margins" required="required" runat="server" placeholder="Prezime"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group">
                                    <label class="control-label">
                                        E-mail
                                    </label>
                                    <div class="controls">
                                        <asp:TextBox ID="mailInput" class="span6 input-left-top-margins" required="required"  runat="server" placeholder="E-mail"></asp:TextBox>

                                        <span class="help-inline ">Unesite e-mail adresu
                                        </span>
                                    </div>
                                </div>

                                <div class="control-group">
                                    <label class="control-label">
                                        Spol
                                    </label>
                                    <div class="controls controls-row">
                                        <asp:DropDownList ID="genderList" class="span4" runat="server">
                                            <asp:ListItem Text="M" Value="M" Selected="true" />
                                            <asp:ListItem Text="Ž" Value="Ž" />
                                        </asp:DropDownList>
                                        <span class="help-inline ">Odaberite spol
                                        </span>
                                    </div>
                                </div>

                                <div class="control-group">
                                    <label class="control-label" for="DateOfBirthMonth">
                                        Datum rođenja
                                    </label>
                                    <div class="controls controls-row">

                                        <asp:DropDownList ID="dayList" class="span4 input-left-top-margins" runat="server">
                                            <asp:ListItem Text="- Dan -" Selected="true" />
                                            <asp:ListItem Text="1" Value="1" />
                                            <asp:ListItem Text="2" Value="2" />
                                            <asp:ListItem Text="3" Value="3" />
                                            <asp:ListItem Text="4" Value="4" />
                                            <asp:ListItem Text="5" Value="5" />
                                            <asp:ListItem Text="6" Value="6" />
                                            <asp:ListItem Text="7" Value="7" />
                                            <asp:ListItem Text="8" Value="8" />
                                            <asp:ListItem Text="9" Value="9" />
                                            <asp:ListItem Text="10" Value="10" />
                                            <asp:ListItem Text="11" Value="11" />
                                            <asp:ListItem Text="12" Value="12" />
                                            <asp:ListItem Text="13" Value="13" />
                                            <asp:ListItem Text="14" Value="14" />
                                            <asp:ListItem Text="15" Value="15" />
                                            <asp:ListItem Text="16" Value="16" />
                                            <asp:ListItem Text="17" Value="17" />
                                            <asp:ListItem Text="18" Value="18" />
                                            <asp:ListItem Text="19" Value="19" />
                                            <asp:ListItem Text="20" Value="20" />
                                            <asp:ListItem Text="21" Value="21" />
                                            <asp:ListItem Text="22" Value="22" />
                                            <asp:ListItem Text="23" Value="23" />
                                            <asp:ListItem Text="24" Value="24" />
                                            <asp:ListItem Text="25" Value="25" />
                                            <asp:ListItem Text="26" Value="26" />
                                            <asp:ListItem Text="27" Value="27" />
                                            <asp:ListItem Text="28" Value="28" />
                                            <asp:ListItem Text="29" Value="29" />
                                            <asp:ListItem Text="30" Value="30" />
                                            <asp:ListItem Text="31" Value="31" />
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="monthList" class="span4" runat="server">
                                            <asp:ListItem Text="- Mjesec -" Selected="true" />
                                            <asp:ListItem Text="Januar" Value="1" />
                                            <asp:ListItem Text="Februar" Value="2" />
                                            <asp:ListItem Text="Mart" Value="3" />
                                            <asp:ListItem Text="April" Value="4" />
                                            <asp:ListItem Text="Maj" Value="5" />
                                            <asp:ListItem Text="Juni" Value="6" />
                                            <asp:ListItem Text="Juli" Value="7" />
                                            <asp:ListItem Text="August" Value="8" />
                                            <asp:ListItem Text="Septembar" Value="9" />
                                            <asp:ListItem Text="Oktobar" Value="10" />
                                            <asp:ListItem Text="Novembar" Value="11" />
                                            <asp:ListItem Text="Decembar" Value="12" />
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="yearList" class="span4 input-left-top-margins" runat="server">
                                            <asp:ListItem Text="- Godina -" Selected="true" />
                                            <asp:ListItem Text="2012" Value="2012" />
                                            <asp:ListItem Text="2011" Value="2011" />
                                            <asp:ListItem Text="2009" Value="2009" />
                                            <asp:ListItem Text="2008" Value="2008" />
                                            <asp:ListItem Text="2007" Value="2007" />
                                            <asp:ListItem Text="2006" Value="2006" />
                                            <asp:ListItem Text="2005" Value="2005" />
                                            <asp:ListItem Text="2004" Value="2004" />
                                            <asp:ListItem Text="2003" Value="2003" />
                                            <asp:ListItem Text="2002" Value="2002" />
                                            <asp:ListItem Text="2001" Value="2001" />
                                            <asp:ListItem Text="2000" Value="2000" />
                                            <asp:ListItem Text="1999" Value="1999" />
                                            <asp:ListItem Text="1998" Value="1998" />
                                            <asp:ListItem Text="1997" Value="1997" />
                                            <asp:ListItem Text="1996" Value="1996" />
                                            <asp:ListItem Text="1995" Value="1995" />
                                            <asp:ListItem Text="1994" Value="1994" />
                                            <asp:ListItem Text="1993" Value="1993" />
                                            <asp:ListItem Text="1992" Value="1992" />
                                            <asp:ListItem Text="1991" Value="1991" />
                                            <asp:ListItem Text="1990" Value="1990" />
                                            <asp:ListItem Text="1989" Value="1989" />
                                            <asp:ListItem Text="1988" Value="1988" />
                                            <asp:ListItem Text="1987" Value="1987" />
                                            <asp:ListItem Text="1986" Value="1986" />
                                            <asp:ListItem Text="1985" Value="1985" />
                                            <asp:ListItem Text="1984" Value="1984" />
                                            <asp:ListItem Text="1983" Value="1983" />
                                            <asp:ListItem Text="1982" Value="1982" />
                                            <asp:ListItem Text="1981" Value="1981" />
                                            <asp:ListItem Text="1980" Value="1980" />
                                            <asp:ListItem Text="1979" Value="1979" />
                                            <asp:ListItem Text="1978" Value="1978" />
                                            <asp:ListItem Text="1977" Value="1977" />
                                            <asp:ListItem Text="1976" Value="1976" />
                                            <asp:ListItem Text="1975" Value="1975" />
                                            <asp:ListItem Text="1974" Value="1974" />
                                            <asp:ListItem Text="1973" Value="1973" />
                                            <asp:ListItem Text="1972" Value="1972" />
                                            <asp:ListItem Text="1971" Value="1971" />
                                            <asp:ListItem Text="1970" Value="1970" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="widget">
                        <div class="widget-header">
                            <div class="title">
                                <span class="fs1" aria-hidden="true" data-icon="&#xe023;"></span>&nbsp;Pristupni podaci
                            </div>
                        </div>
                        <div class="widget-body">
                            <div class="form-horizontal no-margin">
                                <div class="control-group">
                                    <label class="control-label" for="email1">
                                        Korisničko ime
                                    </label>
                                    <div class="controls">
                                        <asp:TextBox ID="usernameInput" class="span12" runat="server" required="required"  placeholder="Korisničko ime"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group">
                                    <label class="control-label" for="password1">
                                        Lozinka
                                    </label>
                                    <div class="controls">
                                        <asp:TextBox ID="password1Input" class="span12" required="required"  runat="server" placeholder="Lozinka" TextMode="Password"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="control-group">
                                    <label class="control-label" for="repPassword">
                                        Potvrda lozinke
                                    </label>
                                    <div class="controls">
                                        <asp:TextBox ID="password2Input" class="span12" required="required"  runat="server" placeholder="Potvrda lozinke" TextMode="Password"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="form-actions no-margin">
                                    <asp:Button ID="createSubmit" class="btn btn-info pull-right" runat="server" Text="Kreiraj korisnički račun" OnClick="createSubmit_Click" />
                                    <asp:Button ID="cancelSubmit" class="btn btn-warning2" CausesValidation="false" runat="server" Text="Odustani" OnClick="cancelSubmit_Click" />
                                    <div class="clearfix">
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
