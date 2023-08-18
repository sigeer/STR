## 通过一些简单的代码处理string（一般来自于http请求）

- [x] select a, b, c from json // 如果要处理的string是json格式，则返回属性为a,b,c的新object(如果json是数组，则返回对象数组，相当于map)，可以通过as重命名属性
- [x] select replace(a, '1', '2') from json //从json中读取a属性，并将1替换成2
- [ ] select text('.a'), html('#app') from dom // 如果要处理的string是html，则返回 class=a的元素的text, id=app的元素的innerHtml, 并组合成新的object
- [ ] insert into demo () values() // 将数据写入到demo表中，没有表则创建表
- [ ] download from xxx // xxx必须是string，或者string[], 将下载这些内容
- [x] 支持子查询
- [ ] request getAttr('#xxx', 'href')
- [ ] select text('.content') from dom if exist('#xxx') then goto getAttr('#xxx', 'href'), loop();