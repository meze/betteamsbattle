﻿var providers,openid;(function(a){openid={version:"1.3",demo:false,demo_text:null,cookie_expires:180,cookie_name:"openid_provider",cookie_path:"/",img_path:"/Scripts/openid-selector/images/",locale:null,sprite:null,signin_text:null,all_small:false,no_sprite:false,image_title:"{provider}",input_id:null,provider_url:null,provider_id:null,init:function(e){providers=a.extend({},providers_large,providers_small);var b=a("#openid_btns");this.input_id=e;a("#openid_choice").show();a("#openid_input_area").empty();var d=0;for(id in providers_large){box=this.getBoxHTML(id,providers_large[id],this.all_small?"small":"large",d++);b.append(box)}if(providers_small){b.append("<br/>");for(id in providers_small){box=this.getBoxHTML(id,providers_small[id],"small",d++);b.append(box)}}a("#openid_form").submit(this.submit);var c=this.readCookie();c&&this.signin(c,true)},getBoxHTML:function(b,c,a,d){if(this.no_sprite){var e=a=="small"?".ico.gif":".gif";return'<a title="'+this.image_title.replace("{provider}",c.name)+'" href="javascript:openid.signin(\''+b+'\');" style="background: #FFF url('+this.img_path+"../images."+a+"/"+b+e+') no-repeat center center" class="'+b+" openid_"+a+'_btn"></a>'}var f=a=="small"?-d*24:-d*100,g=a=="small"?-60:0;return'<a title="'+this.image_title.replace("{provider}",c.name)+'" href="javascript:openid.signin(\''+b+'\');" style="background: #FFF url('+this.img_path+"openid-providers-"+this.sprite+".png); background-position: "+f+"px "+g+'px" class="'+b+" openid_"+a+'_btn"></a>'},signin:function(c,d){var b=providers[c];if(!b)return;this.highlight(c);this.setCookie(c);this.provider_id=c;this.provider_url=b.url;if(b.label)this.useInputBox(b);else{a("#openid_input_area").empty();!d&&a("#openid_form").submit()}},submit:function(){var b=openid.provider_url;if(b){b=b.replace("{username}",a("#openid_username").val());openid.setOpenIdUrl(b)}if(openid.demo){alert(openid.demo_text+"\r\n"+document.getElementById(openid.input_id).value);return false}if(b.indexOf("javascript:")==0){b=b.substr("javascript:".length);eval(b);return false}return true},setOpenIdUrl:function(c){var b=document.getElementById(this.input_id);if(b!=null)b.value=c;else a("#openid_form").append('<input type="hidden" id="'+this.input_id+'" name="'+this.input_id+'" value="'+c+'"/>')},highlight:function(c){var b=a("#openid_highlight");b&&b.replaceWith(a("#openid_highlight a")[0]);a("."+c).wrap('<div id="openid_highlight"></div>')},setCookie:function(c){var a=new Date;a.setTime(a.getTime()+this.cookie_expires*864e5);var b="; expires="+a.toGMTString();document.cookie=this.cookie_name+"="+c+b+"; path="+this.cookie_path},readCookie:function(){for(var c=this.cookie_name+"=",d=document.cookie.split(";"),b=0;b<d.length;b++){var a=d[b];while(a.charAt(0)==" ")a=a.substring(1,a.length);if(a.indexOf(c)==0)return a.substring(c.length,a.length)}return null},useInputBox:function(e){var d=a("#openid_input_area"),c="",b="openid_username",h="",f=e.label,g="";if(f)c="<p>"+f+"</p>";if(e.name=="OpenID"){b=this.input_id;h="http://";g="background: #FFF url("+this.img_path+"openid-inputicon.gif) no-repeat scroll 0 50%; padding-left:18px;"}c+='<input id="'+b+'" type="text" style="'+g+'" name="'+b+'" value="'+h+'" /><input id="openid_submit" type="submit" value="'+this.signin_text+'"/>';d.empty();d.append(c);a("#"+b).focus()},setDemoMode:function(a){this.demo=a}}})(jQuery);var providers_large={google:{name:"Google",url:"https://www.google.com/accounts/o8/id"},yahoo:{name:"Yahoo",url:"http://me.yahoo.com/"},aol:{name:"AOL",label:"Enter your AOL screenname.",url:"http://openid.aol.com/{username}"},myopenid:{name:"MyOpenID",label:"Enter your MyOpenID username.",url:"http://{username}.myopenid.com/"},openid:{name:"OpenID",label:"Enter your OpenID.",url:null}},providers_small={livejournal:{name:"LiveJournal",label:"Enter your Livejournal username.",url:"http://{username}.livejournal.com/"},wordpress:{name:"Wordpress",label:"Enter your Wordpress.com username.",url:"http://{username}.wordpress.com/"},blogger:{name:"Blogger",label:"Your Blogger account",url:"http://{username}.blogspot.com/"},verisign:{name:"Verisign",label:"Your Verisign username",url:"http://{username}.pip.verisignlabs.com/"},claimid:{name:"ClaimID",label:"Your ClaimID username",url:"http://claimid.com/{username}"},clickpass:{name:"ClickPass",label:"Enter your ClickPass username",url:"http://clickpass.com/public/{username}"},google_profile:{name:"Google Profile",label:"Enter your Google Profile username",url:"http://www.google.com/profiles/{username}"}};openid.locale="en";openid.sprite="en";openid.demo_text="In client demo mode. Normally would have submitted OpenID:";openid.signin_text="Sign-In";openid.image_title="log in with {provider}"