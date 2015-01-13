// <!CDATA[
var pop = null;
function ShowIframe(title, contentUrl, width, height)
{
    pop = new Popup({ contentType: 1, isHaveTitle: true, scrollType: true, isBackgroundCanClick: false, isSupportDraging: true, isShowShadow: true, isReloadOnClose: false, width: width, height: height });
    pop.setContent("contentUrl", contentUrl);
    pop.setContent("title", title);
    pop.build();
    pop.show();
}
function ShowHtmlString(title, strHtml, width, height)
{
    pop = new Popup({ contentType: 2, isReloadOnClose: false, width: width, height: height });
    pop.setContent("contentHtml", strHtml);
    pop.setContent("title", title);
    pop.build();
    pop.show();
}
function ShowConfirm(title, confirmCon, id, str, width, height)
{
    var pop = new Popup({ contentType: 3, isReloadOnClose: false, width: width, height: height });
    pop.setContent("title", title);
    pop.setContent("confirmCon", confirmCon);
    pop.setContent("callBack", ShowCallBack);
    pop.setContent("parameter", { id: id, str: str, obj: pop });
    pop.build();
    pop.show();
}
function ShowAlert(title, alertCon, width, height)
{
    pop = new Popup({ contentType: 4,isReloadOnClose: false, width: width, height: height });
    pop.setContent("title", title);
    pop.setContent("alertCon", alertCon);
    pop.build();
    pop.show();
}
function ShowCallBack(para)
{
    var o_pop = para["obj"]
    var obj = document.getElementById(para["id"]);
    o_pop.close();
    obj.click();
}
function ClosePop()
{
    pop.close();
}
// ]]>