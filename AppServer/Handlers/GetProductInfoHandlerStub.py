# coding=utf-8
import json

from Handlers.BaseHandler import BaseHandler


class GetProductInfoHandlerStub(BaseHandler):
    def getAdditives(self):
        result = []
        for i in range(5):
            ad = dict(id=str(100 + i), category=u"Категория", names=[u"Имя1", u"Имя2", u"Имя3"], severity=i,
                      comment=u"Вызывает слонёнка Гобо")
            result.append(ad)
        return result


    def post(self):
        response = {'name': u"Тестовое имя товара", 'additives': self.getAdditives()}
        self.response.set_status(200)
        self.response.write(json.dumps(response, ensure_ascii=False))
