import json
import logging

from Handlers.BaseHandler import BaseHandler


class GetProductInfoHandler(BaseHandler):
    def post(self):
        response = {'key1': 'value1'}
        logging.warning(self.request.body)
        self.response.set_status(200)
        self.response.write(json.dumps(response))