from Handlers.BaseHandler import BaseHandler
import PageParser

class MainPageHandler(BaseHandler):
    def get(self):
        self.render('index.html')