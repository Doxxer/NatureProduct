# coding=utf-8
import json
import logging
import urllib

from Handlers.BaseHandler import BaseHandler
from Model.FoodAdditive import FoodAdditive


class GetAdditiveInfoHandler(BaseHandler):
    @staticmethod
    def find_by_name(name):
        name = urllib.unquote_plus(name).decode("utf-8")
        result = {}
        additive = FoodAdditive.all().filter("names = ", name).fetch(10)
        if additive:
            additive = additive[0]
            assert isinstance(additive, FoodAdditive)
            result['id'] = additive.id
            result['category'] = additive.category
            result['severity'] = additive.severity
            result['names'] = additive.names
            result['comment'] = additive.comment
        return result

    @staticmethod
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
