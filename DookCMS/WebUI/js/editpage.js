document.writeln("<link href=\"/js/lhgdialog/lhgdialog.css\" rel=\"stylesheet\" type=\"text/css\" />");
document.writeln("<scri" + "pt  src=\"/js/lhgdialog/lhgcore.min.js\" type=\"text/javascript\"></scri" + "pt >");
document.writeln("<scri" + "pt  src=\"/js/lhgdialog/lhgdialog.js\" type=\"text/javascript\"></scri" + "pt >");
//document.writeln("<scri" + "pt  src=\"/js/jquery-1.4.2.min.js\" type=\"text/javascript\"></scri" + "pt >");

$(document).ready(function()
{
    var wid = document.body.clientWidth;
    var fdfds = new J.ui.dialog({ id: 'd2',title:'模板编辑工具箱', fixed: true, top: 'top', page: '../../EditTemplate.aspx', left: 'right', width: wid, height: 100, btns: false, drag: false });
    fdfds.ShowDialog();
});
