<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML><HEAD>
<TITLE>.:: www.caesar.elte.hu ::.</TITLE>
<META http-equiv="Content-Type" content="text/HTML; charset=utf-8">
<META name="Description" content="">
<META name="Keywords" content="">

</HEAD>

<BODY bgcolor="#FFFFFF" text="#5B3400" marginwidth="0" marginheight="20" leftmargin="0" topmargin="37">

<DIV align="center" id="content">
<TABLE border="0" cellspacing="0" cellpadding="0" width="347" height="345">
<TR>
  <TD><IMG src="http://www.caesar.elte.hu/dyn/corner_lt.gif" border="0" width="6" height="6"></TD>
  <TD>

      <TABLE background="http://www.caesar.elte.hu/dyn/infinite_top.gif" border="0" cellspacing="0" cellpadding="0" width="347" height="6">
        <TR>
        <TD><IMG src="http://www.caesar.elte.hu/dyn/spacer.gif" border="0" width="347" height="6"></TD>
        </TR>
        </TABLE>
    </TD>
  <TD><IMG src="http://www.caesar.elte.hu/dyn/corner_rt.gif" border="0" width="6" height="6"></TD>
</TR>

<TR>

  <TD valign="top" background="http://www.caesar.elte.hu/dyn/infinite_left.gif">
      <TABLE background="http://www.caesar.elte.hu/dyn/infinite_left.gif" border="0" cellspacing="0" cellpadding="0" width="6" height="333">
        <TR>
        <TD><IMG src="http://www.caesar.elte.hu/dyn/spacer.gif" border="0" width="6" height="333"></TD>
        </TR>
        </TABLE>    
    </TD>
    
    <TD>
      <TABLE border="0" cellspacing="0" cellpadding="0" width="347" height="333">

      <TR>
    <TD colspan="2"><IMG src="http://www.caesar.elte.hu/dyn/header_elte.gif" border="0" width="347" height="47" alt=".: Eötvös Loránd Tudományegyetem :."></TD>
    </TR>

    <TR>    
    <TD>
      <TABLE background="http://www.caesar.elte.hu/dyn/backg_elte.gif" border="0" cellspacing="0" cellpadding="3" width="347" height="333">
          <TR>
              <TD width="17" rowspan="2" valign="top"><IMG src="http://www.caesar.elte.hu/dyn/spacer.gif" border="0" width="17" height="1"></TD>
            <TD valign="top"><FONT face="Georgia, Times New Roman, Times, serif" style="font-size:15pt" color=red><i>HTTP/1.1 404 Not Found</i></FONT></TD>

            </TR>
          <TR>
                <TD valign="top"><FONT face="Arial, Helvetica, sans-serif" style="font-size:10pt" color="#005098" align="justify" valign="top">
      <p align="justify">
      A kért weboldal nem található
      </p>
      <span id="error-code" style="display: none">1</span>
      <span id="error-message"><p align="justify">A hiba okának felderítése folyamatban van.</p></span>
      <p align="justify">
      További információkért keresse fel <a href="http://iig.elte.hu/szervezet/operatorok/">operátori szolgálatunkat</a>.</P>

            </TR>

            
        </TABLE>    
      </TD>
    </TR>
    </TABLE>

    </TD>
    
    <TD valign="top" background="http://www.caesar.elte.hu/dyn/infinite_right.gif">
      <TABLE background="http://www.caesar.elte.hu/dyn/infinite_right.gif" border="0" cellspacing="0" cellpadding="0" width="6" height="333">
        <TR>
        <TD><IMG src="http://www.caesar.elte.hu/dyn/spacer.gif" border="0" width="6" height="333"></TD>
        </TR>
        </TABLE>        
    </TD>
</TR>

<TR>
  <TD><IMG src="http://www.caesar.elte.hu/dyn/corner_lb.gif" border="0" width="6" height="6"></TD>
  <TD>
      <TABLE background="http://www.caesar.elte.hu/dyn/infinite_bottom.gif" border="0" cellspacing="0" cellpadding="0" width="347" height="6">
        <TR>
        <TD><IMG src="http://www.caesar.elte.hu/dyn/spacer.gif" border="0" width="347" height="6"></TD>
        </TR>
        </TABLE>
    </TD>

  <TD><IMG src="http://www.caesar.elte.hu/dyn/corner_rb.gif" border="0" width="6" height="6"></TD>
</TR>
    
<TR>
  <TD colspan="3" height="15" align="center" valign="bottom"><FONT face="Arial, Helvetica, sans-serif" style="font-size:8pt" color="#005098">&copy;2015 ELTE INFORMATIKAI IGAZGATÓSÁG Minden jog fenntartva.</FONT></TD>
</TR>
</TABLE>
</DIV>
<script >
  var loaded; loaded = 0;
  var nprog; nprog = 0;

  function loadingHack(){
    if (loaded == 0)
    {
      nprog++;
      if (nprog > 10)
        nprog = 0;
      var p = "";
      for (var i =0; i < nprog; i++)
      {
        p+=".";
      }
      document.getElementById("error-message").innerHTML="<p align=\"justify\">A hiba okának felderítése folyamatban van."+p+"</p>"; 
      setTimeout(loadingHack, 40);
    }
  }
  setTimeout(loadingHack, 40);
  setTimeout(function(){
    loaded = 1;
    if (document.getElementById("error-code").innerHTML == "1") { 
      document.getElementById("error-message").innerHTML="<p align=\"justify\">A webszerver nem találja a kért fájlt.</p><p align=\"justify\">Amennyiben Ön a keresett honlap tartalmára mutató hivatkozást követve jutott ide, kérjük, jelezze a honlap tulajdonosának a hibát.</p><p align=\"justify\">Amennyiben Ön a honlap tulajdonosa, győződjön meg róla, hogy a kért fájl valóban létezik-e.</p>"; 
    }else if (document.getElementById("error-code").innerHTML == "2") { 
      document.getElementById("error-message").innerHTML="<p align=\"justify\">A honlaphoz tartozó Caesar azonosító nem rendelkezik webszolgáltatással.</p><p align=\"justify\">Amennyiben Ön a honlap tulajdonosa, az info.caesar.elte.hu weboldalra bejelentkezve be tudja kapcsolni a webszolgáltatást.</p>"; 
    }else if (document.getElementById("error-code").innerHTML == "3") { 
      document.getElementById("error-message").innerHTML="<p align=\"justify\">A honlaphoz tartozó Caesar azonosító nem létezik.</p>"; 
    }else if (document.getElementById("error-code").innerHTML == "4") { 
      document.getElementById("error-message").innerHTML="<p align=\"justify\">A probléma oka böngészőhiba miatt nem állapítható meg.</p>"; 
    }
  }, 1000);

</script>

</BODY>
</HTML>
