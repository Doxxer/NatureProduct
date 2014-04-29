from contextlib import closing
from urllib2 import urlopen, HTTPError, URLError
import logging

from bs4 import BeautifulSoup


BASE_URL = "http://goodsmatrix.ru/goods/"


def getPage(url):
    page = ""
    try:
        with closing(urlopen(url)) as url_object:
            page = url_object.read()
    except HTTPError as e:
        logging.warning("HTTPError: {0} : {1}  ".format(e.errno, e.strerror) )
    except URLError as e:
        logging.warning("URLError: {0} : {1}  ".format(e.errno, e.strerror) )

    return page

def getPageTest():
    page = ""
    try:
        with open('../static/static/coca_cola.html', 'r') as f:
            page = f.read()
    except IOError as e:
        print "I/O error({0}): {1}".format(e.errno, e.strerror)

    return page


#Parse data from html = urlopen(url).read()
def parseProductDataTest(html):
    result = []

    soup = BeautifulSoup(html)

    ingredient_table = soup.find(id='Composition')

    if ingredient_table is None:
        logging.warning("There is no ingredients in " + html)
        return []

    for elem in ingredient_table.get_text().split(','):
        result.append(elem.strip())

    return result


def parseProductData(html):
    result = []

    soup = BeautifulSoup(html)

    ingredient_table = soup.find(id='ctl00_ContentPH_Ingredients_IngrDL')

    if ingredient_table is None:
        logging.warning("There is no ingridients in " + html)
        return []

    for elem in ingredient_table.findAll('a'):
        if elem.parent.name == 'td':
            result.append(elem.get_text().strip())

    return result


def getProductDataTest():
    html = getPageTest()
    if html == "":
        logging.warning("Empty Result")
        return []

    return parseProductData(html)


#Main function
#Returns string list of names of ingredients
def getProductData(product_id):
    url = BASE_URL + str(product_id) + ".html"
    html = getPage(url)
    if html == "":
        logging.warning("Empty Result")
        return []

    return parseProductData(html)



def checkStr(s):
    if isinstance(s, str):
        print "ordinary string"
    elif isinstance(s, unicode):
        print "unicode string"
    else:
        print "not a string"

if __name__ == "__main__":

    for ingr in getProductData(4810206001901):
        print ingr