import json
import logging

from Handlers.BaseHandler import BaseHandler
from Model.FoodAdditive import FoodAdditive


class GetProductInfoHandler(BaseHandler):
    def post(self):
        request = self.request.body
        if request.find('=') == -1:
            self.response.set_status(500)

        request = request.split('=')[1].strip()
        if request.lower().startswith('e'):
            request = request[1:].strip()

        logging.info(request)
        response = {}

        result = FoodAdditive.all().filter("id = ", request).fetch(1)
        if result:
            result = result[0]
            assert isinstance(result, FoodAdditive)
            response['id'] = result.id
            response['category'] = result.category
            response['severity'] = result.severity
            response['names'] = result.names
            response['comment'] = result.comment

        self.response.set_status(200)
        self.response.write(json.dumps(response, ensure_ascii=False))