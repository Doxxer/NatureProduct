# -*- coding: utf-8 -*-
from contextlib import closing
from urllib2 import urlopen, HTTPError, URLError
import logging
import re

additive_reg =  re.compile(u'[еЕeE]\s{0,}([0-9]{3,4}[a-zA-Z]{0,1})', re.UNICODE)

from bs4 import BeautifulSoup


BASE_URL = "http://goodsmatrix.ru/mobile/"


def get_product_data(barcode):

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

    def parse_product_data_mobile(html):
        result = []

        soup = BeautifulSoup(html)

        ingredient_table = soup.find(id='Composition')

        if ingredient_table is None:
            logging.warning("There is no ingredients in " + html)
            return []

        for elem in ingredient_table.get_text().split(','):
            current_additive_list = additive_reg.findall(elem)
            if not current_additive_list:
                result.append(elem.strip())
            else:
                for additive in current_additive_list:
                    result.append(additive)

        return result


    def parse_product_data(page_content):
        result = []

        soup = BeautifulSoup(page_content)

        ingredient_table = soup.find(id='ctl00_ContentPH_Ingredients_IngrDL')

        if ingredient_table is None:
            logging.warning("There is no ingredients in " + page_content)
            return []

        for elem in ingredient_table.findAll('a'):
            if elem.parent.name == 'td':
                caption = elem.get_text().strip()
                result.append(caption)

        return result

    url = BASE_URL + str(barcode) + ".html"
    html = get_page_content(url)
    if html == "":
        logging.warning("Empty Result")
        return []
    return parse_product_data_mobile(html)



if __name__ == "__main__":
    for ingr in get_product_data(4810206001901):
        print ingr