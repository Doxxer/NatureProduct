from contextlib import closing
from urllib2 import urlopen
import re


BASE_URL = "http://goodsmatrix.ru/goods/"

def getPage(url):
    page = ""
    with closing( urlopen(url) ) as url_object:
        page = url_object.read()
	return page

def getProductData(product_id):
    try:
        url = BASE_URL + str(product_id) + ".html"
        html = getPage(url)
        #TODO parse html

    except:
        print "Exception"



