# coding=utf-8
import json
import logging
import urllib

from Handlers.BaseHandler import BaseHandler
from Model.FoodAdditive import FoodAdditive
from PageParser import  getProductData


def find_by_name(name):
    name = urllib.unquote_plus(name).decode("utf-8")
    result = {}
    additive = FoodAdditive.all().filter("names in ", [name]).fetch(10)
    if additive:
        additive = additive[0]
        assert isinstance(additive, FoodAdditive)
        result['id'] = additive.id
        result['category'] = additive.category
        result['severity'] = additive.severity
        result['names'] = additive.names
        result['comment'] = additive.comment
    return result


def find_by_id(code):
    result = {}
    additive = FoodAdditive.all().filter("id = ", code).fetch(1)
    if additive:
        additive = additive[0]
        assert isinstance(additive, FoodAdditive)
        result['id'] = additive.id
        result['category'] = additive.category
        result['severity'] = additive.severity
        result['names'] = additive.names
        result['comment'] = additive.comment
    return result


def getAdditiveList(bar_code):
    for ingredient in getProductData(bar_code):
        if ingredient.lower().startswith('e'):
            ingredient = ingredient[1:].strip()
            logging.info(ingredient)






class GetAdditiveInfoHandler(BaseHandler):
    def post(self):
        request = self.request.body
        if request.find('=') == -1:
            self.response.set_status(500)

        request = request.split('=')[1].strip()
        if request.lower().startswith('e'):
            request = request[1:].strip()

        logging.info(request)

        response = self.find_by_id(request)
        if not response:
            response = self.find_by_name(request)

        self.response.set_status(200)
        self.response.write(json.dumps(response, ensure_ascii=False))
