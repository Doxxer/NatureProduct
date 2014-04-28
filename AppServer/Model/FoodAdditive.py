from google.appengine.ext import db


class FoodAdditive(db.Model):
    id = db.StringProperty(required=True)
    category = db.StringProperty(required=True)
    names = db.StringListProperty(required=True)
    severity = db.IntegerProperty(required=True)
    comment = db.TextProperty()