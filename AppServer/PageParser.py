from contextlib import closing
from urllib2 import urlopen, HTTPError, URLError
import logging

from bs4 import BeautifulSoup


BASE_URL = "http://goodsmatrix.ru/goods/"


def get_page_content(url):
    page = ""
    try:
        with closing(urlopen(url)) as request:
            if request.headers.get('content-type'):
                encoding = request.headers['content-type'].split('charset=')[-1]
            else:
                encoding = 'windows-1251'
            page = unicode(request.read(), encoding)
    except HTTPError as e:
        logging.warning("HTTPError: {0} : {1}  ".format(e.errno, e.strerror))
    except URLError as e:
        logging.warning("URLError: {0} : {1}  ".format(e.errno, e.strerror))

    return page


def parse_product_data(html):
    result = []

    soup = BeautifulSoup(html)

    ingredient_table = soup.find(id='ctl00_ContentPH_Ingredients_IngrDL')

    if ingredient_table is None:
        logging.warning("There is no ingredients in " + html)
        return []

    for elem in ingredient_table.findAll('a'):
        if elem.parent.name == 'td':
            caption = elem.get_text().strip()
            result.append(caption)

    return result


def get_product_data(barcode):
    url = BASE_URL + str(barcode) + ".html"
    html = get_page_content(url)
    if html == "":
        logging.warning("Empty Result")
        return []
    return parse_product_data(html)


if __name__ == "__main__":
    for ingr in get_product_data(4810206001901):
        print ingr