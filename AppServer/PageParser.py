# -*- coding: utf-8 -*-
from contextlib import closing
from urllib2 import urlopen, HTTPError, URLError
import logging
import re

additive_reg = re.compile(u'[еЕeE]\s{0,}([0-9]{3,4}[a-zA-Z]{0,1})', re.UNICODE)

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

    def parse_product_data(page_content):
        result = []
        soup = BeautifulSoup(page_content)
        ingredient_table = soup.find(id='Composition')
        if ingredient_table is None:
            logging.warning("There is no ingredients")
            return []

        for elem in ingredient_table.get_text().split(','):
            current_additive_list = additive_reg.findall(elem)
            if not current_additive_list:
                result.append(elem.strip())
            else:
                for additive in current_additive_list:
                    result.append(additive)

        return result

    def get_name(page_content):
        soup = BeautifulSoup(page_content)
        span_name = soup.find(id='GoodsName')
        if span_name is None:
            logging.warning("There is no name")
            return ""
        return span_name.get_text().strip()

    if barcode.strip().startswith('2'):
        return "2", []

    url = BASE_URL + str(barcode) + ".html"
    content = get_page_content(url)
    if content == "":
        logging.warning("Empty Result")
        return "1", []

    name_result, ecomp = get_name(content), parse_product_data(content)
    if not name_result:
        return "3", []
    return name_result, ecomp


if __name__ == "__main__":
    name, lst = get_product_data("4810206001901")
    print name
    for ingr in lst:
        print ingr