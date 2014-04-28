import json
import logging
import urllib2

from Handlers.BaseHandler import BaseHandler


class GetProductInfoHandler(BaseHandler):
    def post(self):
        logging.warning(self.request.body)
        response = {'key1': 'value1'}
        self.response.set_status(200)
        self.response.write(json.dumps(response))