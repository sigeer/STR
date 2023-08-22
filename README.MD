## 通过一些简单的代码处理string（一般来自于http请求）

- [x] select a, b, c from json // 如果要处理的string是json格式，则返回属性为a,b,c的新object(如果json是数组，则返回对象数组，相当于map)，可以通过as重命名属性
- [x] select replace(a, '1', '2') from json //从json中读取a属性，并将1替换成2
- [ ] insert into demo () values() // 将数据写入到demo表中，没有表则创建表
- [ ] download from xxx // xxx必须是string，或者string[], 将下载这些内容
- [ ] request getAttr('#xxx', 'href')
- [x] select text('a') from (select b from datasource) // 支持复合查询，比如json的属性值是html (完成一半，目前功能是通过2次执行实现的)；支持子查询
- [ ] select text('.content') from dom if exist('#xxx') then goto getAttr('#xxx', 'href'), loop();