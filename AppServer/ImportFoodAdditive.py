# coding=utf-8
import logging

from xlrd.sheet import Sheet
import xlrd
from Model.FoodAdditive import FoodAdditive

additive_database = 'base.xls'


def get_all_food_additives():
    class ParseError(Exception):
        pass

    def parse_id(value):
        try:
            assert value
            assert isinstance(value, unicode)
            assert value[0] == 'E'
        except AssertionError:
            raise ParseError
        else:
            return value.strip()[1:]

    def parse_category(value):
        try:
            assert value
            assert isinstance(value, unicode)
        except AssertionError:
            raise ParseError
        else:
            return value.strip()

    def parse_names(value):
        try:
            assert value
            assert isinstance(value, unicode)
            if value.find(':') != -1:
                value = value[value.find(':') + 1:].strip()
            names = value.replace(',', ';').replace(')', ';').replace('(', ';').split(';')
            names = [x.strip() for x in names]
        except AssertionError:
            raise ParseError
        else:
            return names

    def parse_severity(color):
        assert isinstance(color, int)
        if color == 50:
            return 0
        elif color == 51:
            return 1
        elif color == 52:
            return 2
        elif color == 53:
            return 3
        elif color == 10:
            return 4
        raise ParseError

    def parse_comment(value):
        try:
            if value:
                assert isinstance(value, unicode)
        except AssertionError:
            raise ParseError
        else:
            return value.strip()

    result = []

    book = xlrd.open_workbook(additive_database, formatting_info=True)
    sheet = book.sheets()[0]
    assert isinstance(sheet, Sheet)
    for row in range(sheet.nrows):
        try:
            f = FoodAdditive(id=parse_id(sheet.cell(row, 1).value),
                             category=parse_category(sheet.cell(row, 2).value),
                             names=parse_names(sheet.cell(row, 3).value),
                             severity=parse_severity(
                                 book.xf_list[sheet.cell_xf_index(row, 0)].background.pattern_colour_index),
                             comment=parse_comment(sheet.cell(row, 4).value))
        except ParseError:
            pass
        else:
            result.append(f)
    return result


def run_import():
    """
    Если база данных добавок была пуста, то запускается парсинг xml файла и запись всех сущностей в базу данных

    """
    if not FoodAdditive.all().fetch(1):
        logging.info("import food additives to local database... ")
        for e in get_all_food_additives():
            assert isinstance(e, FoodAdditive)
            e.put()
        logging.info("import food additives to local database... Done")
    else:
        logging.info("food additives database is OK")


if __name__ == "__main__":
    for k, e in enumerate(get_all_food_additives()):
        print k, e