# coding=utf-8
import logging
import urllib

from Model.FoodAdditive import FoodAdditive
from PageParser import get_product_data


def find_by_name(name):
    name = urllib.unquote_plus(name).decode("utf-8")
    result = {}
    additive = FoodAdditive.all().filter("names in ", [name]).fetch(10)
    if additive:
        additive = additive[0]
        assert isinstance(additive, FoodAdditive)
        result['id'] = additive.id
        result['category'] = additive.category
        result['severity'] = additive.severity
        result['names'] = additive.names
        result['comment'] = additive.comment
    return result


def find_by_id(code):
    result = {}
    additive = FoodAdditive.all().filter("id = ", code).fetch(1)
    if additive:
        additive = additive[0]
        assert isinstance(additive, FoodAdditive)
        result['id'] = additive.id
        result['category'] = additive.category
        result['severity'] = additive.severity
        result['names'] = additive.names
        result['comment'] = additive.comment
    return result


def get_additives_list(bar_code):
    result = []
    for ingredient in get_product_data(bar_code):
        if ingredient.lower().startswith('e'):
            ingredient = ingredient[1:].strip()
            logging.info(ingredient)

        additive = find_by_id(ingredient)
        if not additive:
            additive = find_by_name(ingredient)
        if additive:
            result.append(additive)