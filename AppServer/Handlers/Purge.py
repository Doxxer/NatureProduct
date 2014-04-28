from google.appengine.ext import db
from Handlers.BaseHandler import BaseHandler


class Purge(BaseHandler):
    def get(self):
        q = db.GqlQuery("SELECT * FROM FoodAdditive")
        db.delete(q)
        self.response.set_status(200)
        self.response.write("OK")