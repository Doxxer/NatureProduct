import os

import jinja2
import webapp2

from Handlers.MainPageHandler import MainPageHandler


JINJA_ENVIRONMENT = jinja2.Environment(
    loader=jinja2.FileSystemLoader(os.path.join(os.path.dirname(__file__), 'templates')),
    extensions=['jinja2.ext.autoescape'])

app = webapp2.WSGIApplication(
    [
        ('/', MainPageHandler)
    ], debug=True)