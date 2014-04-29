import os

import jinja2
import webapp2
from Handlers.GetAdditiveInfoHandler import GetAdditiveInfoHandler

from Handlers.GetProductInfoHandler import GetProductInfoHandler
from Handlers.MainPageHandler import MainPageHandler
from Handlers.Purge import Purge
import ImportFoodAdditive


JINJA_ENVIRONMENT = jinja2.Environment(
    loader=jinja2.FileSystemLoader(os.path.join(os.path.dirname(__file__), 'templates')),
    extensions=['jinja2.ext.autoescape'])

ImportFoodAdditive.run_import()

app = webapp2.WSGIApplication(
    routes=[
        ('/', MainPageHandler),
        ('/gobo', GetProductInfoHandler),
        ('/gete', GetAdditiveInfoHandler),
        ('/purge', Purge)],
    debug=True)