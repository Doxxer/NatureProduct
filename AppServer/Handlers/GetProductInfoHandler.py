# coding=utf-8
import json
import logging

from GetAdditiveInfo import get_additive
from Handlers.BaseHandler import BaseHandler
from PageParser import get_product_data


class GetProductInfoHandler(BaseHandler):
    def getAdditives(self, ingredients):
        result = []
        for ingredient in ingredients:
            additive = get_additive(ingredient)
            if additive:
                result.append(additive)
        return result

    def post(self):
        logging.warning(self.request)
        logging.warning(self.request.body)
        barcode = self.request.body
        if barcode.find('=') == -1:
            self.response.set_status(500)

        barcode = barcode.split('=')[1].strip()

        name, ingredients = get_product_data(barcode)

        response = {'name': name, 'additives': self.getAdditives(ingredients)}
        self.response.set_status(200)
        self.response.write(json.dumps(response, ensure_ascii=False))