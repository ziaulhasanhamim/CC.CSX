﻿using CC.CSX;
using static CC.CSX.HtmlElements;
using static CC.CSX.HtmlAttributeKeys;
using static CC.CSX.HtmxAttributeKeys;

var node = Template(
  Script(@"
  function hello() {
    alert('hello');
  }
  // window.addEventListener('load', hello);"),
  Div(
    H1((id, "test"), (hxPut, "https://google.com"), "Hello world", "Zdravo"),
    MainConent(),
    A((href, "https://google.com"), (hxGet, "/do-something"), (@class, "test"), (style, "color: red;font:bold;"), "Lets go"),
    Span((style, "color: red;"), "Hello world")));

node.ApplyWhen(x => true, x =>
{
    x.Attributes.ForEach(x => Print(x.Name + (x?.Value ?? "N/A")));
    x.Value = x?.Value?.ToUpper();
});

Console.WriteLine(node.When(x => x is HtmlNode node && node.Attributes.Any(x => x.Name == "style")).Count());
Print(node);

HtmlNode Scripts(params HtmlNode[] children) => Head(children);

HtmlNode MainConent() => Article(
    (id, "article-1"),
    (hxGet, "/articles/1"),
    (hxSwap, "outerHTML"),
    (hxTrigger, "load"),
    (hxTarget, "#article-1"),
    "Text",
    P("This is a paragraph"),
    P("This is another paragraph"),
    P("This is a third paragraph"));

HtmlNode Template(HtmlNode head, HtmlNode root, string mode = "light") =>
  Html((style, mode is "dark" ? "background-color: black" : "background-color: white;"),
    Scripts(head),
    Body(root));

void Print(object node) => Console.WriteLine(node.ToString());