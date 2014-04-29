# coding=utf-8

from Model.FoodAdditive import FoodAdditive


def get_additive(ingredient):
    def find_by_name(name):
        name = name.strip()
        result = {}
        additive = FoodAdditive.all().filter("names = ", name).fetch(1)
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

    additive = find_by_id(ingredient)
    if not additive:
        additive = find_by_name(ingredient)
    return additive