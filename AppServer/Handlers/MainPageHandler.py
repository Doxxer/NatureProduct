from Handlers.BaseHandler import BaseHandler

class MainPageHandler(BaseHandler):
    def get(self):
        self.render('index.html')