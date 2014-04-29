from Handlers.BaseHandler import BaseHandler
from GetAdditiveInfo import *
from PageParser import get_product_data

class MainPageHandler(BaseHandler):
    def get(self):
        print "TestAdditive"
        TestAdditive()
        self.render('index.html')