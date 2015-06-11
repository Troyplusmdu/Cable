
function nbToggleBlock(elem, closed, open) 
{
	if(elem.getAttribute("expanded") == null) {
		return;
	}
	else
	{
		var isExpanded = (elem.getAttribute("expanded") == "true");
		
		if(isExpanded == true) {
			return;
		}
		else
		{
			var block = elem.parentElement;
			var navbar = block.parentElement;
			
			var allblocks = navbar.getElementsByTagName("div");
			var expanded;
			
			for(var i=0; i<allblocks.length; i++) {
				if(allblocks[i].getAttribute("expanded") != null && allblocks[i].getAttribute("expanded") == "true") 
				{
					expanded = allblocks[i];
					break;
				}
			}
			
			if(expanded != null) {
				expanded.setAttribute("expanded", "false");
				expanded.className = closed;
				var childdiv = expanded.parentElement.childNodes[1];
				if(childdiv != null) {
					childdiv.style.display = "none";
				}
			}
			
			expanded = block.childNodes[0];
			if(expanded != null) {
				expanded.className = open;
				expanded.setAttribute("expanded", "true");
				var now = new Date();
				fixDate(now);
				now.setTime(now.getTime() + 30 * 60 * 1000);
				nbsetCookie("Expanded", block.id, now);
			}
			
			var openingdiv = block.childNodes[1];
			if(openingdiv != null) {
				openingdiv.style.display = "block";
			}
		}
	}
}

function nbItemHighlight(elem, style) 
{
	elem.className = style;
	elem.parentElement.className = style;
}

function nbItemLowlight(elem, style) 
{
	elem.className = style;
	elem.parentElement.className = style;
}

function setSelectedCookie(elem, style) {
	var now = new Date();
	fixDate(now);
	now.setTime(now.getTime() + 30 * 60 * 1000);
	nbsetCookie("Selected", elem, now);
	var ctl = document.getElementById(elem);
	if(ctl!=null) {
		ctl.className = style;
	}
}
function nbReplace(elem, url, style) 
{
	setSelectedCookie(elem, style);
	window.location.replace(unescape(url));
}

function nbNavigate(elem, url, style) 
{
	setSelectedCookie(elem, style);
	window.location.href = unescape(url);
}

function nbsetCookie(name, value, expires, path, domain, secure) {
  var curCookie = name + "=" + escape(value) +
      ((expires) ? "; expires=" + expires.toGMTString() : "") +
      "; path=/;";
  document.cookie = curCookie;
}


function nbgetCookie(name) {
  var dc = document.cookie;
  var prefix = name + "=";
  var begin = dc.indexOf("; " + prefix);
  if (begin == -1) {
    begin = dc.indexOf(prefix);
    if (begin != 0) return null;
  } else
    begin += 2;
  var end = document.cookie.indexOf(";", begin);
  if (end == -1)
    end = dc.length;
  return unescape(dc.substring(begin + prefix.length, end));
}

function nbdeleteCookie(name, domain) {
  if (nbgetCookie(name)) {
    document.cookie = name + "=" +
    ((domain) ? "; domain=" + domain : "") +
    "; expires=Thu, 01-Jan-70 00:00:01 GMT";
  }
}

function fixDate(date) {
  var base = new Date(0);
  var skew = base.getTime();
  if (skew > 0)
    date.setTime(date.getTime() - skew);
}

function clickButton(e, buttonid)
{ 
  var evt = e ? e : window.event;
  var bt = document.getElementById(buttonid);

  if (bt)
  { 
      if (evt.keyCode == 13)
      { 
         bt.click(); 
         return false; 
      } 
  } 
  
}

function getTags(tagName) 
{ 
        if (!document.getElementsByTagName) return null; 
                return document.getElementsByTagName(tagName); 
} 

function SetSize() 
{ 
        var divs = getTags('div'); 
        
        for (var i = 0; i < divs.length; i++) 
        { 
                if (divs[i].className.toLowerCase() == 'divbody') 
                { 
                        divs[i].style.height = document.documentElement.clientWidth / 2.25; 
                } 
        } 
}       

function GettxtBoxName(txt)
{
	var tags = getTags("input");
	if (tags == null) return null;
	
	for (i = 0; i < tags.length; i++)
	{	
		if(tags[i].name.indexOf(txt) != -1)
		    return tags[i].name;
	}
	
}

function CustomersSearch()
{

    var querystring =''; 
    var ddllist = '';
    
    var tags = getTags("select");
    if (tags == null) 
        return null;
    	
    for (i = 0; i < tags.length; i++)
    {	
	    if(tags[i].name.indexOf("ddArea") != -1)
	        ddllist = document.getElementById( tags[i].name );
    }

    var cust = GettxtBoxName("txtCustName");

    var custVal = document.getElementById(cust).value;
    
    if(custVal == '')
    {
		alert('Minimum search criteria not met. Please enter the Customer Name')
		return;
    }

    var itemName= ddllist.options[ddllist.selectedIndex].value;
    
    if(itemName == '0')
    {
		alert('Minimum search criteria not met. Please select Area.')
		return;
    }
    
    
    if(itemName.indexOf("'") > -1 )
    {
		itemName = itemName.replace("'","^");
    }
    
    querystring = 'area='+ itemName +'&name=' + custVal;
    
    window.showModalDialog('DisconCustomers.aspx?' + querystring ,self,'dialogWidth:600px;dialogHeight:350px;status:no;dialogHide:yes;unadorned:yes;' );
    return false;

}

function clock()
{
  var time = new Date()
  var hr = time.getHours()
  var min = time.getMinutes()
  var sec = time.getSeconds()
  
                        
   var temp = "" + ((hr > 12) ? hr - 12 : hr)
   if(hr==0) temp = "12"
   if(temp.length==1) temp = " " + temp
   temp += ((min < 10) ? ":0" : ":") + min
   temp += ((sec < 10) ? ":0" : ":") + sec
   temp += (hr >= 12) ? " PM" : " AM"
  if(hr < 10){
    hr = " " + hr
    }
  if(min < 10){
    min = "0" + min
    }
  if(sec < 10){
    sec = "0" + sec
    } 
    
  document.getElementById('ctl00_lblTitle').innerHTML = temp
  setTimeout("clock()", 1000)
          
}


function ConfirmCancel() 
{
    var txtAmount = GettxtBoxName("txtAmount");
    var Amount = document.getElementById(txtAmount).value;

    var txtCustBal = GettxtBoxName("txtCustBalance");
    var Balance = document.getElementById(txtCustBal).value;
   
    var hiddenVal = document.getElementById("ctl00_cplhControlPanel_hidRetVal").value;
    Balance = Balance - Amount;
    
    if( Balance < 0 )
    {
        var rv = confirm("Balance will go Negative. Are you sure you want to continue with this Cash Entry?"); 
        
        if(rv == true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    else
    {
        return true;
    }
}

function ValidatePaidDate() 
{ 
    var l_dt = new Date(); 
    
    var today = Date.UTC( l_dt.getFullYear(), l_dt.getMonth()+1, l_dt.getDate(),0,0,0); 
    
    
    var txtDatePaid = GettxtBoxName("txtDatePaid");
    var Ldt_due = document.getElementById(txtDatePaid).value;
    
    //isDate(Ldt_due)
    
    if (isDate(Ldt_due)==false){
		return false
	}
    
    var Ldt_Year,Ldt_Month,Ldt_Day 
    Ldt_Year = String(Ldt_due).substring(6,10) 
    

    if ( String(Ldt_due).charAt(6) == "/" ) 
    { 
            Ldt_Month = String(Ldt_due).substring(3,5); 
            
            if ( String(Ldt_due).charAt(8) == " " ) 
            { 
                    Ldt_Day = String(Ldt_due).substring(8,9); 
            } 
            else 
            { 
                    Ldt_Day = String(Ldt_due).substring(8,9); 
            } 
    }//Char At6 
    else 
    { 
            Ldt_Month = String(Ldt_due).substring(3,5); 
            if ( String(Ldt_due).charAt(9) == " " ) 
            { 
                    Ldt_Day = String(Ldt_due).substring(8,9); 
            } 
            else{ 
            Ldt_Day = String(Ldt_due).substring(0,2); 
            } 
    }//Else

    starttime = Date.UTC( Ldt_Year, Ldt_Month , Ldt_Day,0,0,0); 
    
    //alert((starttime - today)/(1000*60*60*24)); 
    
    if ( (starttime - today)/(1000*60*60*24) > 0 ) 
    {
        alert("Date Paid should be cannot be greater than Todays Date.")
        return false
    }
    
    if ( (starttime - today)/(1000*60*60*24) < - 30 ) 
    { 
        var rv = confirm("Date Paid is more than 30 days old. Are you sure you want to continue with this Cash Entry?"); 
        
        if(rv == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    } 
    
}

/**
 * DHTML date validation script for dd/mm/yyyy. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
 */
// Declaring valid date character, minimum year and maximum year
var dtCh= "/";
var minYear=1900;
var maxYear=2100;

function isInteger(s){
	var i;
    for (i = 0; i < s.length; i++){   
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag){
	var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++){   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary (year){
	// February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}
function DaysArray(n) {
	for (var i = 1; i <= n; i++) {
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		if (i==2) {this[i] = 29}
   } 
   return this
}

function isDate(dtStr)
{
	var daysInMonth = DaysArray(12)
	var pos1=dtStr.indexOf(dtCh)
	var pos2=dtStr.indexOf(dtCh,pos1+1)
	var strDay=dtStr.substring(0,pos1)
	var strMonth=dtStr.substring(pos1+1,pos2)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	}
	month=parseInt(strMonth)
	day=parseInt(strDay)
	year=parseInt(strYr)
	if (pos1==-1 || pos2==-1){
		alert("The date format should be : dd/mm/yyyy")
		return false
	}
	if (strMonth.length<1 || month<1 || month>12){
		alert("Please enter a valid month")
		return false
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		alert("Please enter a valid day")
		return false
	}
	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
		alert("Please enter a valid 4 digit year between "+minYear+" and "+maxYear)
		return false
	}
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
		alert("Please enter a valid date")
		return false
	}
    return true
}

function ValidateForm(){
	var dt=document.frmSample.txtDate
	if (isDate(dt.value)==false){
		dt.focus()
		return false
	}
    return true
 }

function getTags(tagName) 
{ 
        if (!document.getElementsByTagName) return null; 
                return document.getElementsByTagName(tagName); 
} 
