from contextlib import closing
from urllib2 import urlopen
import logging
from bs4 import BeautifulSoup


BASE_URL = "http://goodsmatrix.ru/goods/"


def getPage(url):
    page = ""
    with closing(urlopen(url)) as url_object:
        page = url_object.read()
    return page


def getPageTest():
    page = ""
    try:
        with open('static/coca_cola.html', 'r') as f:
            page = f.read()
    except IOError as e:
        print "I/O error({0}): {1}".format(e.errno, e.strerror)

    return page

#Parse data from html = urlopen(url).read()
def parseProductData(html):
    result = []

    soup = BeautifulSoup(html)

    print soup.prettify()

    ingredient_table = soup.find(id='Composition')

    if ingredient_table == None:
        logging.warning("Empty Result")
        return result

    for elem in ingredient_table.get_text().split(','):
        result.append(elem.strip())
        logging.warning(elem.strip())

    return result

#Parse data from static html file
def parseProductDataStatic(html):
    result = []

    soup = BeautifulSoup(html)

    #print soup.prettify()

    ingredient_table = soup.find(id='ctl00_ContentPH_Ingredients_IngrDL')

    if ingredient_table == None:
        logging.warning("Empty Result")
        return result

    for elem in ingredient_table.findAll('a'):
        if elem.parent.name == 'td':
            result.append(elem.get_text())
            logging.warning(elem.get_text())

    return result


#Main function
#Returns string list of names of ingredients
def getProductData(product_id):
    url = BASE_URL + str(product_id) + ".html"
    html = getPage(url)
    if html == "":
        logging.warning("Empty Result")
        return []

    return parseProductData(html)
