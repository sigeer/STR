## ͨ��һЩ�򵥵Ĵ��봦��string��һ��������http����

- [x] select a, b, c from json // ���Ҫ������string��json��ʽ���򷵻�����Ϊa,b,c����object(���json�����飬�򷵻ض������飬�൱��map)������ͨ��as����������
- [x] select replace(a, '1', '2') from json //��json�ж�ȡa���ԣ�����1�滻��2
- [ ] select text('.a'), html('#app') from dom // ���Ҫ������string��html���򷵻� class=a��Ԫ�ص�text, id=app��Ԫ�ص�innerHtml, ����ϳ��µ�object
- [ ] insert into demo () values() // ������д�뵽demo���У�û�б��򴴽���
- [ ] download from xxx // xxx������string������string[], ��������Щ����
- [x] ֧���Ӳ�ѯ
- [ ] request getAttr('#xxx', 'href')
- [ ] select text('.content') from dom if exist('#xxx') then goto getAttr('#xxx', 'href'), loop();