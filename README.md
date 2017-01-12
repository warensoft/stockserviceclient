<div id="cnblogs_post_body"><p align="center">Warensoft Stock Service Api客户端接口说明</p>
<p align="center">Warensoft Stock Service Api Client Reference</p>
<p>&nbsp;</p>
<ol>
<li>可使用环境(Available Environments)</li>
</ol>
<p>本客户端被编译为.net standard 1.6。支持的运行环境如下：</p>
<p>This client was compiled to .net standard 1.6, and the follow runtime are supported:</p>
<table border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td valign="top" width="155">
<p align="center"><strong>运行时</strong></p>
</td>
<td valign="top" width="374">
<p align="center"><strong>版本</strong></p>
</td>
</tr>
<tr>
<td valign="top" width="155">
<p>.net standard</p>
</td>
<td valign="top" width="374">
<p>1.6+</p>
</td>
</tr>
<tr>
<td valign="top" width="155">
<p>.net framework</p>
</td>
<td valign="top" width="374">
<p>4.6.1+</p>
</td>
</tr>
<tr>
<td valign="top" width="155">
<p>Portable</p>
</td>
<td valign="top" width="374">
<p>259</p>
</td>
</tr>
<tr>
<td valign="top" width="155">
<p>Xamarin</p>
</td>
<td valign="top" width="374">
<p>&nbsp;</p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<ol>
<li>安装（Setup）</li>
</ol>
<p>本客户端需要通过Nuget方式进行安装，用户可以在Nuget中搜索“Warensoft”，并选择安装Warensoft.EntLib.StockServiceClient如果客户端需要使用MVVM模式，则可以选择安装Warensoft.EntLib.Common，如下图所示：</p>
<p>This client needs to be installed through Nuget. Users could find this component by typing “Warensoft”, and then install “Warensoft.EntLib.StockServiceClient”. If your client needs MVVM pattern support, you could also install “Warensoft.EntLib.Common”:</p>
<p align="center"><img src="http://images2015.cnblogs.com/blog/105209/201701/105209-20170112181619838-850284817.png" alt=""></p>
<p>&nbsp;</p>
<p align="center">&nbsp;</p>
<ol>
<li>接口支持的功能（2017.1.12版本）(Methods supported by this Client)</li>
</ol>
<p>其类图如下所示：</p>
<p>The class diagram is shown as bellow:</p>
<p align="center"><img src="http://images2015.cnblogs.com/blog/105209/201701/105209-20170112181633994-1731783121.png" alt=""></p>
<p>&nbsp;</p>
<p align="center">&nbsp;</p>
<p>功能清单如下：</p>
<p>The Methods are listed as bellow:</p>
<table style="width: 100%;" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td valign="top" width="22%">
<p align="center"><strong>接口名</strong></p>
</td>
<td valign="top" width="77%">
<p align="center"><strong>说明</strong></p>
</td>
</tr>
<tr>
<td valign="top" width="22%">
<p>GetATR</p>
</td>
<td valign="top" width="77%">
<p>根据K线获取平均真实波幅曲线</p>
<p>Get ATR indicator</p>
</td>
</tr>
<tr>
<td valign="top" width="22%">
<p>GetEMA</p>
</td>
<td valign="top" width="77%">
<p>根据K线获取指数平均数指标</p>
<p>Get EMA indicator</p>
</td>
</tr>
<tr>
<td valign="top" width="22%">
<p>GetKline</p>
</td>
<td valign="top" width="77%">
<p>根据Ticker值获取其K线</p>
<p>Get K line by ticker</p>
</td>
</tr>
<tr>
<td valign="top" width="22%">
<p>GetMACD</p>
</td>
<td valign="top" width="77%">
<p>据K线获取指数平滑移动平均线</p>
<p>Get MACD indicator</p>
</td>
</tr>
<tr>
<td valign="top" width="22%">
<p>GetRSI</p>
</td>
<td valign="top" width="77%">
<p>据K线获取相对强弱指标</p>
<p>Get RSI indicator</p>
</td>
</tr>
<tr>
<td valign="top" width="22%">
<p>GetSAR</p>
</td>
<td valign="top" width="77%">
<p>据K线获取抛物线指标</p>
<p>Get SAR indicator</p>
</td>
</tr>
<tr>
<td valign="top" width="22%">
<p>GetSMA</p>
</td>
<td valign="top" width="77%">
<p>据K线获取简单平均数指标</p>
<p>Get SMA indicator</p>
</td>
</tr>
<tr>
<td valign="top" width="22%">
<p>GetWR</p>
</td>
<td valign="top" width="77%">
<p>据K线获取Williams %R指标</p>
<p>Get Williams %R indicator</p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<ol>
<li>Api调用方法(Invoking API)</li>
</ol>
<ul>
<li>初始化客户端驱动,此处使用的是测试用AppKey和SecretKey.(Initializing the client driver. Notice: the AppKey and SecretKey are test values, so DO NOT use them in Production Environment)</li>
</ul>
<p align="left">&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</p>
<div class="cnblogs_code">
<pre><span style="color: #0000ff;">var</span> driver = <span style="color: #0000ff;">new</span> StockServiceDriver(<span style="color: #800000;">"</span><span style="color: #800000;">C6651783-A3B9-4B72-8B02-A2E67A59C5A6</span><span style="color: #800000;">"</span>, <span style="color: #800000;">"</span><span style="color: #800000;">6C442B3AF58D4DDA81BB03B353C0D7D8</span><span style="color: #800000;">"</span>);</pre>
</div>
<p>&nbsp;</p>
<ul>
<li>&nbsp;获取目标K线(Obtain the target k lines)</li>
</ul>
<div class="cnblogs_code">
<pre>List&lt;Kline&gt; kline = LoadKline();</pre>
</div>
<p>&nbsp;</p>
<ul>
<li>调用所需接口(Invoke the interface you need)</li>
</ul>
<div class="cnblogs_code">
<pre><span style="color: #0000ff;">var</span> atr=driver.GetATR(kline,<span style="color: #800080;">10</span><span style="color: #000000;">);
</span><span style="color: #0000ff;">var</span> ema=drvier.GetEMA(kline,<span style="color: #800080;">10</span><span style="color: #000000;">);
…</span></pre>
</div>
<p>&nbsp;</p></div>
